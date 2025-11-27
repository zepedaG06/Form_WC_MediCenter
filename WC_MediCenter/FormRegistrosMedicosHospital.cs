using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormRegistrosMedicosHospital : Form
    {
        private Sistema sistema;
        private string idHospital;
        private ListBox listBoxRegistros;

        public FormRegistrosMedicosHospital(Sistema sistemaParam, string idHospitalParam)
        {
            sistema = sistemaParam;
            idHospital = idHospitalParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "Registros Médicos del Hospital";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Registros Médicos del Hospital";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(250, 20);
            lblTitulo.Size = new Size(400, 40);
            this.Controls.Add(lblTitulo);

            listBoxRegistros = new ListBox();
            listBoxRegistros.Location = new Point(50, 80);
            listBoxRegistros.Size = new Size(800, 550);
            listBoxRegistros.Font = new Font("Consolas", 10);
            this.Controls.Add(listBoxRegistros);

            CargarRegistros();

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(390, 640);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void CargarRegistros()
        {
            if (!sistema.RegistrosPorHospital.ContainsKey(idHospital))
            {
                listBoxRegistros.Items.Add("No hay registros en este hospital");
                return;
            }

            List<RegistroMedico> registros = sistema.RegistrosPorHospital[idHospital];

            if (registros.Count == 0)
            {
                listBoxRegistros.Items.Add("No hay registros en este hospital");
                return;
            }

            foreach (var registro in registros)
            {
                listBoxRegistros.Items.Add("───────────────────────────────────────────────");
                listBoxRegistros.Items.Add($"ID Registro: {registro.IdRegistro}");
                listBoxRegistros.Items.Add($"Fecha: {registro.Fecha:dd/MM/yyyy HH:mm}");
                listBoxRegistros.Items.Add($"Hospital: {registro.IdHospital}");
                listBoxRegistros.Items.Add($"Síntomas: {string.Join(", ", registro.Sintomas)}");
                listBoxRegistros.Items.Add($"Diagnóstico: {registro.Diagnostico}");
                if (!string.IsNullOrEmpty(registro.Tratamiento))
                    listBoxRegistros.Items.Add($"Tratamiento: {registro.Tratamiento}");
                listBoxRegistros.Items.Add(
                    $"Estado: {(registro.Confirmado ? "Confirmado" : "Pendiente")}");
                if (!string.IsNullOrEmpty(registro.IdMedico))
                    listBoxRegistros.Items.Add($"Médico: {registro.IdMedico}");
                if (!string.IsNullOrEmpty(registro.ObservacionDoctor))
                    listBoxRegistros.Items.Add($"Observaciones: {registro.ObservacionDoctor}");
                listBoxRegistros.Items.Add("");
            }
        }
    }
}