using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpie.Aion
{
    class Target
    {
        public int Level { get; set;}
        public int CurrentHP { get; set; }
        public int HPPercent { get; set; }
        public int MaxHP { get; set; }
        public int State { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public bool validTarget { get; set; }

        public Target()
        {
            Update();
        }

        public void Update()
        {
            if (AionMemory.readInt((long)Offsets.Player.HasTarget + AionMemory.base_adress) == 1)
            {
                this.CurrentHP = AionMemory.readInt((long)Offsets.Target.CurrentHP);
                this.HPPercent = AionMemory.readByte((long)Offsets.Target.HPPercent);
                this.Level = AionMemory.readByte((long)Offsets.Target.Level);
                this.MaxHP = AionMemory.readInt((long)Offsets.Target.MaxHP);
                this.State = AionMemory.readInt((long)Offsets.Target.HideStatus);
                this.Name = AionMemory.ReadString((long)Offsets.Target.HideStatus);
                Position p = new Position();
                p.X = AionMemory.readFloat((long)Offsets.Target.xPos);
                p.Y = AionMemory.readFloat((long)Offsets.Target.yPos);
                p.Z = AionMemory.readFloat((long)Offsets.Target.zPos);
                this.Position = p;
                validTarget = true;
            }
            else
            {
                validTarget = false;
            }
        }
    }
}
