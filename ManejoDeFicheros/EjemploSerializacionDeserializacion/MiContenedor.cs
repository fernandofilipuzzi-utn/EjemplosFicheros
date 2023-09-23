using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploSerializacionDeserializacion
{
    [Serializable]
    class MiContenedor
    {
        List<Persona> Listado = new List<Persona>();
        static public int Numero { get; set; } = 99;
        public void Agregar(Persona valor){ Listado.Add(valor); }

        public int Cantidad { get { return Listado.Count; } }

        public Persona this[int n]
        {
            get { return Listado[n];  }
        }
    }
}
