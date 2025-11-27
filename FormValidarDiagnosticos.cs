using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormValidarDiagnosticos : Form
    {
        private Sistema sistema;
        private PersonalHospitalario medico;
        private ListBox listBoxPendientes;

        public FormValidarDiagnosticos(Sistema sistemaParam, PersonalHospitalario medicoParam)
        {
            sistema = sistemaParam;
            medico = medicoParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "Validar Diagnósticos Pendientes";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Validar Diagnósticos Pendientes";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(250, 20);
            lblTitulo.Size = new Size(400, 40);
            this.Controls.Add(lblTitulo);

            listBoxPendientes = new ListBox();
            listBoxPendientes.Location = new Point(50, 80);
            listBoxPendientes.Size = new Size(800, 550);
            listBoxPendientes.Font = new Font("Consolas", 10);
            this.Controls.Add(listBoxPendientes);

            CargarPendientes();

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(390, 640);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void CargarPendientes()
        {
            if (!sistema.RegistrosPorHospital.ContainsKey(medico.IdHospital))
            {
                listBoxPendientes.Items.Add("No hay registros en este hospital");
                return;
            }

            List<RegistroMedico> pendientes = sistema.RegistrosPorHospital[medico.IdHospital]
                .FindAll(r => !r.Confirmado);

            if (pendientes.Count == 0)
            {
                listBoxPendientes.Items.Add("Todos los diagnósticos están confirmados");
                return;
            }

            listBoxPendientes.Items.Add($"Diagnósticos pendientes: {pendientes.Count}");
            listBoxPendientes.Items.Add("═══════════════════════════════════════════════");
            listBoxPendientes.Items.Add("");

            foreach (var registro in pendientes)
            {
                listBoxPendientes.Items.Add("───────────────────────────────────────────────");
                listBoxPendientes.Items.Add($"ID Registro: {registro.IdRegistro}");
                listBoxPendientes.Items.Add($"Fecha: {registro.Fecha:dd/MM/yyyy HH:mm}");
                listBoxPendientes.Items.Add($"Hospital: {registro.IdHospital}");
                listBoxPendientes.Items.Add($"Síntomas: {string.Join(", ", registro.Sintomas)}");
                listBoxPendientes.Items.Add($"Diagnóstico preliminar: {registro.Diagnostico}");
                listBoxPendientes.Items.Add("");
            }
        }
    }
}