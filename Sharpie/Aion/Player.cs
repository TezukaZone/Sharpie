using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpie.Aion
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentEXP { get; set; }
        public int MaxEXP { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxMP { get; set; }
        public bool HasTarget { get; set; }
        public Position Position { get; set; }

        public Player()
        {
            Update();
        }

        public void Update()
        {
            this.Name = AionMemory.ReadString((long)AionMemory.base_adress + (long)Offsets.Player.Name);
            this.Level = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.Level);
            this.CurrentEXP = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.CurrentEXP);
            this.MaxEXP = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.MaxEXP);
            this.CurrentHP = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.CurrentHP);
            this.MaxHP = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.MaxHP);
            this.CurrentMP = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.CurrentMP);
            this.MaxMP = AionMemory.readInt((long)AionMemory.base_adress + (long)Offsets.Player.MaxMP);
            this.HasTarget = AionMemory.readInt((long)Offsets.Player.HasTarget + AionMemory.base_adress) == 1 ? true : false;
            Position p = new Position();
            p.X = AionMemory.readFloat((long)AionMemory.base_adress + (long)Offsets.Player.xPos);
            p.Y = AionMemory.readFloat((long)AionMemory.base_adress + (long)Offsets.Player.yPos);
            p.Z = AionMemory.readFloat((long)AionMemory.base_adress + (long)Offsets.Player.zPos);
        }
        
    }
}
