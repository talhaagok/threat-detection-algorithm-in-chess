using System;
using System.Collections.Generic;
using System.Text;


namespace cs_tech_satranc
{
    class Taslar
    {
        //HER BİR NESNENİN TÜRÜNE GÖRE PUANLANMASI
        public int puan(string letter)
        {
            int stone_point = 0;
            switch (letter[0])
            {
                case 'k':
                    letter = "kale";
                    stone_point = 5;
                    break;
                case 'a':
                    letter = "at";
                    stone_point = 3;
                    break;
                case 'f':
                    letter = "fil";
                    stone_point = 3;
                    break;
                case 'v':
                    letter = "vezir";
                    stone_point = 9;
                    break;
                case 's':
                    letter = "sah";
                    stone_point = 100;
                    break;
                case 'p':
                    letter = "piyon";
                    stone_point = 1;
                    break;
                default:
                    break;
            }

            return stone_point;
        }

        public string[] stone_info = new string[2];
        public string[] movement(string letter, string color, int[] isim, string[,] all_array)
        {
            //Piyonlarda Renge Göre Yön Değiştiği için Ayrı Hareket Kodlaması Yapılmıştır.
           //Beyaz Piyon Hareketleri
            if (letter == "piyon" && color == "beyaz")
            {
                string[] yenidizi = new string[2];
                yenidizi[0] = (isim[0] - 1).ToString() + (isim[1] - 1).ToString();
                yenidizi[1] = (isim[0] - 1).ToString() + (isim[1] + 1).ToString();
                return yenidizi;
            }
            //Siyah Piyon Hareketleri
            else if (letter == "piyon" && color == "siyah")
            {
                string[] yenidizi = new string[2];
                yenidizi[0] = (isim[0] + 1).ToString() + (isim[1] - 1).ToString();
                yenidizi[1] = (isim[0] + 1).ToString() + (isim[1] + 1).ToString();
                return yenidizi;
            }
            //Vezir Hareketleri
            else if (letter == "vezir")
            {
                int vez_x = isim[0];
                int vez_y = isim[1];

                string[] mov_vez = new string[8];
                //Vezir Kuzey Hareket
                if (vez_x -1 != -1)
                {
                    while (vez_x != 0 && all_array[vez_x - 1, vez_y] == "--")
                    {
                        vez_x--;
                    }
                    if (vez_x != 0 && all_array[vez_x - 1, vez_y] != "--")
                    {
                        vez_x--;
                        mov_vez[0] = (vez_x).ToString() + (vez_y).ToString();
                    }
                }
                
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Kuzey Batı Hareket
                while (vez_x != 0 && vez_y != 0 && all_array[vez_x - 1, vez_y -1] == "--")
                {
                    vez_x--;
                    vez_y--;
                }
                if (vez_x != 0 && vez_y != 0 && all_array[vez_x - 1, vez_y -1] != "--")
                {
                    vez_x--;
                    vez_y--;
                    mov_vez[1] = (vez_x).ToString() + vez_y.ToString();
                }
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Batı Hareket
                while (vez_y != 0 && all_array[vez_x, vez_y -1] == "--")
                {
                    vez_y--;
                }
                if (vez_y != 0 && all_array[vez_x, vez_y -1] != "--")
                {
                    vez_y--;
                    mov_vez[2] = (vez_x).ToString() + (vez_y).ToString();
                }
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Güney Batı Hareket
                while (vez_x != 7 && vez_y != 0 && all_array[vez_x + 1, vez_y - 1] == "--")
                {
                    vez_x++;
                    vez_y--;
                }
                if (vez_x != 7 && vez_y != 0 && all_array[vez_x + 1, vez_y - 1] != "--")
                {
                    vez_x++;
                    vez_y--;
                    mov_vez[3] = (vez_x).ToString() + (vez_y).ToString();
                }
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Güney Hareket
                while (vez_x != 7 && all_array[vez_x + 1, vez_y] == "--")
                {
                    vez_x++;
                }
                if (vez_x != 7 && all_array[vez_x + 1, vez_y] != "--")
                {
                    vez_x++;
                    mov_vez[4] = (vez_x).ToString() + (vez_y).ToString();
                }
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Güney Doğu Hareket
                while (vez_y != 7 && vez_x != 7 && all_array[vez_x + 1, vez_y + 1] == "--")
                {
                    vez_x++;
                    vez_y++;
                }
                if (vez_y != 7 && vez_x != 7 && all_array[vez_x + 1, vez_y + 1] != "--")
                {
                    vez_x++;
                    vez_y++;
                    mov_vez[5] = (vez_x).ToString() + (vez_y).ToString();
                }
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Doğu Hareket
                while (vez_y != 7 && all_array[vez_x, vez_y + 1] == "--")
                {
                    vez_y++;
                }
                if (vez_y != 7 && all_array[vez_x, vez_y + 1] != "--")
                {
                    vez_y++;
                    mov_vez[6] = (vez_x).ToString() + (vez_y).ToString();
                }
                vez_x = isim[0];
                vez_y = isim[1];
                //Vezir Kuzey Doğu Hareket
                while (vez_x != 0 && vez_y != 7 && all_array[vez_x - 1, vez_y + 1] == "--")
                {
                    vez_x--;
                    vez_y++;
                }
                if (vez_x != 0 && vez_y != 7 && all_array[vez_x - 1, vez_y + 1] != "--")
                {
                    vez_x--;
                    vez_y++;
                    mov_vez[7] = (vez_x).ToString() + (vez_y).ToString();
                }

                return mov_vez;
            }
            //At Hareketleri
            else if (letter == "at")
            {
                string[] mov_at = new string[8];
                mov_at[0] = (isim[0] - 2).ToString() + (isim[1] - 1).ToString();
                mov_at[1] = (isim[0] - 1).ToString() + (isim[1] - 2).ToString();
                mov_at[2] = (isim[0] + 1).ToString() + (isim[1] - 2).ToString();
                mov_at[3] = (isim[0] + 2).ToString() + (isim[1] - 1).ToString();
                mov_at[4] = (isim[0] + 2).ToString() + (isim[1] + 1).ToString();
                mov_at[5] = (isim[0] + 1).ToString() + (isim[1] + 2).ToString();
                mov_at[6] = (isim[0] - 1).ToString() + (isim[1] + 2).ToString();
                mov_at[7] = (isim[0] - 2).ToString() + (isim[1] + 1).ToString();
                return mov_at;
            }

            string[] def_let = new string[2];
            return def_let;
        }

        private string color, letter;
        public Taslar(int location, string val_of_txt, string[,] all_array)
        {
            //Taşın Türü Öğreniliyor
            switch (val_of_txt[0])
            {
                case 'k':
                    letter = "kale";
                    break;
                case 'a':
                    letter = "at";
                    break;
                case 'f':
                    letter = "fil";
                    break;
                case 'v':
                    letter = "vezir";
                    break;
                case 's':
                    letter = "sah";
                    break;
                case 'p':
                    letter = "piyon";
                    break;
                default:
                    break;
            }
            //Taşın Rengi Öğreniliyor
            if (val_of_txt[1] == 's')
            {
                color = "siyah";
            }
            else if(val_of_txt[1] == 'b')
            {
                color = "beyaz";
            }
            //TAŞ KOORDİNATI ÖĞRENİLİP MOVEMENT FONKSİYONUNA GÖNDERİLİYOR
            int[] array_loc = new int[2];
            array_loc[0] = (location - (location % 10)) / 10;
            array_loc[1] = location % 10;

            stone_info = movement(letter, color, array_loc, all_array);
        }
    }
}