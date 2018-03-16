using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dino_ENTITY;

namespace dino_SERVICE{
    public static class DinoService{
        public static ICharacter Harold;
        public static List<ICharacter> MasterCharacterList = new List<ICharacter> {
            new ICharacter() {
                Name = "Tambarine",
                Speed = 300,
                FormImageName = "DINO_1",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Bongo",
                Speed = 300,
                FormImageName = "DINO_2",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            }
        };

        public static List<string> GetAvailableCharacters() {
            List<string> local = MasterCharacterList.Where(m=>m.IsAvailable.Equals(true)).Select(i => i.Name).ToList();
            return local;
        }        
    }
}
