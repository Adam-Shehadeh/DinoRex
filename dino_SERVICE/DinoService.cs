using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dino_ENTITY;

namespace dino_SERVICE{
    public static class DinoService{
        public static List<ICharacter> MasterCharacterList = new List<ICharacter> {
            new ICharacter() {
                Name = "Tambarine",
                Speed = 175,
                FormImageName = "DINO_1",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Bongo",
                Speed = 200,
                FormImageName = "DINO_2",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Flak and Jack",
                Speed = 205,
                FormImageName = "DINO_3",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Harold",
                Speed = 200,
                FormImageName = "DINO_4",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Foxy",
                Speed = 175,
                FormImageName = "FOXY",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Pikachu",
                Speed = 225,
                FormImageName = "PIKACHU",
                Direction = CharacterDirection.Right,
                IsAvailable = true
            },
            new ICharacter() {
                Name = "Naruto Uzumaki",
                Speed = 150,
                FormImageName = "NARUTO",
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
