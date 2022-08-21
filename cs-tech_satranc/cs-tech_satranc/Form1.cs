using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace cs_tech_satranc
{
    public partial class Form1 : Form
    {
        //TXT DOSYASININ YOLUNUN BELİRTİLDİĞİ BÖLÜM
        static readonly string textFile = @"txt\board1.txt";
        public Form1()
        {
            InitializeComponent();
            //ARRAY VE DEĞER ATAMALARININ OLUŞTURULDUĞU BÖLÜM
            //ARRAY TX'DEN GELEN VERİLERİ TUTUYOR
            string[,] array = new string[8, 8];
            int a = 0;
            int b = 0;

            float beyaz_puan = 0;
            float siyah_puan = 0;
            //ARRAY_LOC KONUMLARI TUTUYOR
            int[,] array_loc = new int[8, 8];

            // TXT DOSYASININ SATIRLARININ OKUNDUĞU BÖLÜM
            string[] lines = File.ReadAllLines(textFile);
            foreach (string line in lines)
            {
                //DİZİ SATIRI TUTUYOR VE BOŞLUKLARA GÖRE İTEM AYRIMI YAPIYOR
                string[] dizi = new string[8];
                dizi = line.Split(' ');
                //STRING VE INDEX MATRISININ DOLDURULDUĞU ALAN
                foreach (string x in dizi)
                {
                    array[a, b] = x;
                    array_loc[a, b] = a * 10 + b;
                    b++;
                }
                if (b == 8)
                {
                    a++; b = 0;
                }
            }
            //TEHDİT EDİLEN TAŞLARIN KOORDİNATLARI
            List<string> siyah_tehdit = new List<string>();
            List<string> beyaz_tehdit = new List<string>();
            //HER BİR TAŞ İÇİN NESNE OLUŞTURULUYOR
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (array[i, j] != "--")
                    {
                        Taslar tasslar = new Taslar(array_loc[i, j], array[i, j], array);
                        //TAŞLARIN PUAN ATAMALARI YAPILIYOR
                        int puan = tasslar.puan(array[i, j]);
                        if(array[i,j][1] == 's')
                        {
                            siyah_puan += puan;
                        }
                        else if(array[i,j][1] == 'b')
                        {
                            beyaz_puan += puan;
                        }
                        //RENKLERİNE GÖRE TEHDİT LİSTELERİNE ATAMA YAPILIYOR
                        //VE MATRIS DIŞINDA Kİ DEĞERLER LİSTEDEN ÇIKARTILIYOR
                        if (array[i, j][1] == 's')
                        {
                            siyah_tehdit.AddRange(tasslar.stone_info);
                            siyah_tehdit.RemoveAll(u => u == null);
                            siyah_tehdit.RemoveAll(u => u.Contains("-"));
                            siyah_tehdit.RemoveAll(u => u.Contains("8"));
                            siyah_tehdit.RemoveAll(u => u.Contains("9"));
                        }
                        else if (array[i, j][1] == 'b')
                        {
                            beyaz_tehdit.AddRange(tasslar.stone_info);
                            beyaz_tehdit.RemoveAll(u => u == null);
                            beyaz_tehdit.RemoveAll(u => u.Contains("-"));
                            beyaz_tehdit.RemoveAll(u => u.Contains("8"));
                            beyaz_tehdit.RemoveAll(u => u.Contains("9"));
                        }
                    }
                }
            }
            //BİR TAŞIN BİRDEN FAZLA YERDEN TEHDİT EDİLMESİ DURUMLARI TEKE İNDİRİLİYOR
            siyah_tehdit = siyah_tehdit.Distinct().ToList();
            beyaz_tehdit = beyaz_tehdit.Distinct().ToList();
            //FORM İSMİ VE FORM ÜZERİNDE GÖRSEL BUTONLAR OLUŞTURULUYOR
            this.Text = "Talha GÖK";
            Button[,] button = new Button[8, 8];
            int top = 0;
            int left = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    button[i, j] = new Button();
                    button[i, j].Width = 70;
                    button[i, j].Height = 70;
                    button[i, j].Left = left;
                    button[i, j].Top = top;
                    button[i, j].Enabled = false;
                    //OLUŞTURULAN BUTONLARA PNG ATAMASI
                    switch (array[i, j])
                    {
                        case "ks":
                            button[i, j].Image = Image.FromFile(@"pieces\kal.png");
                            break;
                        case "as":
                            button[i, j].Image = Image.FromFile(@"pieces\at.png");
                            break;
                        case "fs":
                            button[i, j].Image = Image.FromFile(@"pieces\fil.png");
                            break;
                        case "vs":
                            button[i, j].Image = Image.FromFile(@"pieces\vez.png");
                            break;
                        case "ss":
                            button[i, j].Image = Image.FromFile(@"pieces\sah.png");
                            break;
                        case "ps":
                            button[i, j].Image = Image.FromFile(@"pieces\piy.png");
                            break;
                        case "kb":
                            button[i, j].Image = Image.FromFile(@"pieces\kalb.png");
                            break;
                        case "ab":
                            button[i, j].Image = Image.FromFile(@"pieces\atb.png");
                            break;
                        case "fb":
                            button[i, j].Image = Image.FromFile(@"pieces\filb.png");
                            break;
                        case "vb":
                            button[i, j].Image = Image.FromFile(@"pieces\vezb.png");
                            break;
                        case "sb":
                            button[i, j].Image = Image.FromFile(@"pieces\sahb.png");
                            break;
                        case "pb":
                            button[i, j].Image = Image.FromFile(@"pieces\piyb.png");
                            break;
                        default:
                            break;
                    }
                    button[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                    //BUTON ARKA PLAN RENKLERİNİN ATANDIĞI BÖLÜM
                    this.Controls.Add(button[i, j]);
                    left += 70;
                    if ((i + j) % 2 == 0)
                    {
                        button[i, j].BackColor = Color.DarkGray;
                    }
                    else
                    {
                        button[i, j].BackColor = Color.White;
                    }
                }
                top += 70;
                left = 0;
            }

            //TEHDİT ALTINDA Kİ TAŞLARIN PUAN HESAPLAMALARI YAPILIYOR
            double b_half_stone_point = 0;
            double s_half_stone_point = 0;
            //TEHDİT ALTINDA Kİ SİYAH TAŞLARIN PUAN HESAPLAMASI
            foreach (string item in siyah_tehdit)
            {
                int item2 = Convert.ToInt32(item);
                int c, d;
                c = item2 / 10;
                d = item2 % 10;
                if (array[c, d][1] == 'b')
                {
                    button[c, d].BackColor = Color.Red;
                    char letter = array[c, d][0];

                    switch (letter)
                    {
                        case 'k':
                            b_half_stone_point += 2.5;
                            break;
                        case 'a':
                            b_half_stone_point += 1.5;
                            break;
                        case 'f':
                            b_half_stone_point += 1.5;
                            break;
                        case 'v':
                            b_half_stone_point += 4.5;
                            break;
                        case 's':
                            b_half_stone_point += 50;
                            break;
                        case 'p':
                            b_half_stone_point += 0.5;
                            break;
                        default:
                            break;
                    }
                }
            }
            //TEHDİT ALTINDA Kİ BEYAZ TAŞLARIN PUAN HESAPLAMASI
            foreach (string item in beyaz_tehdit)
            {
                int item2 = Convert.ToInt32(item);
                int c, d;
                c = item2 / 10;
                d = item2 % 10;
                if (array[c, d][1] == 's')
                {
                    button[c, d].BackColor = Color.Red;
                    char letter = array[c, d][0];

                    switch (letter)
                    {
                        case 'k':
                            s_half_stone_point += 2.5;
                            break;
                        case 'a':
                            s_half_stone_point += 1.5;
                            break;
                        case 'f':
                            s_half_stone_point += 1.5;
                            break;
                        case 'v':
                            s_half_stone_point += 4.5;
                            break;
                        case 's':
                            s_half_stone_point += 50;
                            break;
                        case 'p':
                            s_half_stone_point += 0.5;
                            break;
                        default:
                            break;
                    }
                }
            }
            //TXT DOSYASINA YAZDIRILACAK SONUÇ SATIRI
            string g_sonuc = textFile.Substring(4,10) + "\t\t" +
                "Siyah Sonuç: " + (siyah_puan - s_half_stone_point).ToString() + "\t" +
                "Beyaz Sonuç: " + (beyaz_puan - b_half_stone_point).ToString() + "\n";

            // KLASÖR YOLU
            string folder = @"txt\";
            // TXT DOSYA ADI
            string fileName = "sonuclar.txt";
            // KLASÖR YOLU VE DOSYA ADI BİRLEŞTİRİLME BÖLÜMÜ
            string fullPath = folder + fileName;
            //YUKARIDA OKUŞTURULAN SONUÇ SATIRININ TXT YE YAZDIRILDIĞI BÖLÜM
            File.AppendAllText(fullPath, g_sonuc);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
