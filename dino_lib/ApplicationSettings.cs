using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dino_DATA {
    public class ApplicationSettings {
        public double interval1 { get; set; }
        public double interval2 { get; set; }
        public bool enabled = true;

        public ApplicationSettings() { } //For XML Serializer

        public ApplicationSettings(double i1, double i2) {
            interval1 = i1;
            interval2 = i2;
        }

        public ApplicationSettings(double i1, double i2, bool active) {
            interval1 = i1;
            interval2 = i2;
            enabled = active;
        }
    }
}
