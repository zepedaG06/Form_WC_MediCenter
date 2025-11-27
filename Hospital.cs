using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class Hospital
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public bool EsPublico { get; set; }
        public decimal CostoConsulta { get; set; }
        public List<string> PersonalIds { get; set; }
        public List<string> PacientesAtendidos { get; set; }
        public int PrecisionDiagnostico { get; set; }
        public int TiempoPromedioMin { get; set; }

        public Hospital()
        {
            PersonalIds = new List<string>();
            PacientesAtendidos = new List<string>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"\n  [{Id}] {Nombre}");
            Console.WriteLine($"       Tipo: {(EsPublico ? "Hospital Publico" : "Hospital Privado")}");
            if (!EsPublico)
                Console.WriteLine($"       Costo Consulta: ${CostoConsulta:F2}");
            Console.WriteLine($"       Precision: {PrecisionDiagnostico}% | Tiempo: {TiempoPromedioMin} min");
            Console.WriteLine($"       Personal: {PersonalIds.Count} | Pacientes: {PacientesAtendidos.Count}");
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre}";
        }
    }
}
