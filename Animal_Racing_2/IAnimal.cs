﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Animal_Racing_2
{
    public interface IAnimal
    {
        string Specie { get; set; }
        string Name { get; set; }

        int NumberOfLegs { get; set; }

        Image ImgAnimal { get; set; }

        string Says();
        void MakeSomeNoise();

        Task<bool> MakeSomeNoiseAsync();
    }
}
