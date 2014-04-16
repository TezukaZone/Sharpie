using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpie.Aion
{
    class Offsets
    {
        internal enum Player
        {
            CurrentEXP = 0x11FE8B8,//4.5
            MaxEXP = 0x11FE8A8,//4.5
            CurrentMP = 0x11FE8D0,//4.5
            MaxMP = 0x11F4A4C,//4.5
            CurrentHP = 0x11FE8C8,//4.5
            MaxHP = 0x11FE8C4,//4.5
            Name = 0x11F47FC,//4.5
            xPos = 0x11EA344,
            yPos = 0x11F7658,//4.5
            zPos = 0x11EA348,
            Level = 0x11F48A0,//4.5
            HasTarget = 0xDB61FC,//4.5
            //Hotbar first slot = Game.dll+126EFB8; // + 0x000008 to keep moving 
        }

        internal enum Target
        {
            Pointer = 0xDA9110,
            Base = Pointer + 0x368,
            Name = Base + 0x46,
            CurrentHP = Base + 0x13ccc,
            MaxHP = Base + 0x13c8,
            HPPercent = Base + 0x44,
            Level = Base + 0x42,
            HideStatus = Base + 0x378,
            CurrentMana = Base + 0x13d0,
            MaxMana = Base + 0x13d4,
            xPos = Base + 0x48,
            yPos = Base + 0x40,
            zPos = Base + 0x44

        }
    }
}
