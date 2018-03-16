using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace dino_ENTITY {
    public enum CharacterDirection {
        Left,
        Right
    }
    public class ICharacter {
        public string Name { get; set; }
        public int Speed { get; set; }
        public string FormImageName { get; set; }
        public CharacterDirection Direction { get; set; }
        public bool IsAvailable { get; set; }
    }
}
