using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormAtenderPaciente : Form
    {
        private Sistema sistema;
        private PersonalHospitalario medico;
        private Paciente paciente;
        private RegistroMedico registro;
        private TextBox txtDiagnostico;
        private TextBox txtTratamiento;
        private TextBox txtObservaciones;

        public FormAtenderPaciente(Sistema sistemaParam, PersonalHospitalario medicoParam)
        {
            sistema = sistemaParam;
            medico = medicoParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 750);
            this.Text = "Atender Paciente";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            if (!sistema.ColasPorHospital.ContainsKey(medico.IdHospital) ||
                sistema.ColasPorHospital[medico.IdHospital].Count == 0)
            {
                Label lblSinPacientes = new Label();
                lblSinPacientes.Text = "No hay pacientes en cola";
                lblSinPacientes.Font = new Font("Segoe UI", 14);
                lblSinPacientes.Location = new Point(300, 300);
                lblSinPacientes.Size = new Size(300, 40);
                this.Controls.Add(lblSinPacientes);

                Button btnCerrar1 = new Button();
                btnCerrar1.Text = "Cerrar";
                btnCerrar1.Font = new Font("Segoe UI", 12);
                btnCerrar1.Location = new Point(390, 360);
                btnCerrar1.Size = new Size(120, 45);
                btnCerrar1.BackColor = Color.White;
                btnCerrar1.Click += (s, e) => this.Close();
                this.Controls.Add(btnCerrar1);
                return;
            }

            string clave = sistema.ColasPorHospital[medico.IdHospital].Dequeue();
            string[] partes = clave.Split('|');
            string idPaciente = partes[0];
            string idRegistro = partes[1];

            paciente = sistema.Pacientes.Find(p => p.Id == idPaciente);
            registro = sistema.RegistrosPorHospital[medico.IdHospital]
                .Find(r => r.IdRegistro == idRegistro);

            if (paciente == null || registro == null)
            {
                MessageBox.Show("Error al cargar datos del paciente", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            Label lblTitulo = new Label();
            lblTitulo.Text = "Atender Paciente";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(330, 20);
            lblTitulo.Size = new Size(240, 40);
            this.Controls.Add(lblTitulo);

            Label lblInfoPaciente = new Label();
            lblInfoPaciente.Text =
                $"Paciente: {paciente.Nombre}\nID: {paciente.Id} | Edad: {paciente.Edad}";
            lblInfoPaciente.Font = new Font("Segoe UI", 11);
            lblInfoPaciente.Location = new Point(50, 80);
            lblInfoPaciente.Size = new Size(400, 50);
            this.Controls.Add(lblInfoPaciente);

            ListBox listRegistro = new ListBox();
            listRegistro.Location = new Point(50, 140);
            listRegistro.Size = new Size(800, 200);
            listRegistro.Font = new Font("Consolas", 10);
            listRegistro.Items.Add("───────────────────────────────────────────────");
            listRegistro.Items.Add($"ID Registro: {registro.IdRegistro}");
            listRegistro.Items.Add($"Fecha: {registro.Fecha:dd/MM/yyyy HH:mm}");
            listRegistro.Items.Add($"Síntomas: {string.Join(", ", registro.Sintomas)}");
            listRegistro.Items.Add($"Diagnóstico: {registro.Diagnostico}");
            this.Controls.Add(listRegistro);

            int yPos = 360;

            Label lblDiagnostico = new Label();
            lblDiagnostico.Text = "Diagnóstico Final:";
            lblDiagnostico.Font = new Font("Segoe UI", 11);
            lblDiagnostico.Location = new Point(50, yPos);
            lblDiagnostico.Size = new Size(150, 25);
            this.Controls.Add(lblDiagnostico);

            txtDiagnostico = new TextBox();
            txtDiagnostico.Font = new Font("Segoe UI", 10);
            txtDiagnostico.Location = new Point(220, yPos);
            txtDiagnostico.Size = new Size(630, 25);
            txtDiagnostico.Text = registro.Diagnostico;
            this.Controls.Add(txtDiagnostico);

            yPos += 40;

            Label lblTratamiento = new Label();
            lblTratamiento.Text = "Tratamiento:";
            lblTratamiento.Font = new Font("Segoe UI", 11);
            lblTratamiento.Location = new Point(50, yPos);
            lblTratamiento.Size = new Size(150, 25);
            this.Controls.Add(lblTratamiento);

            txtTratamiento = new TextBox();
            txtTratamiento.Font = new Font("Segoe UI", 10);
            txtTratamiento.Location = new Point(220, yPos);
            txtTratamiento.Size = new Size(630, 25);
            this.Controls.Add(txtTratamiento);

            yPos += 40;

            Label lblObservaciones = new Label();
            lblObservaciones.Text = "Observaciones:";
            lblObservaciones.Font = new Font("Segoe UI", 11);
            lblObservaciones.Location = new Point(50, yPos);
            lblObservaciones.Size = new Size(150, 25);
            this.Controls.Add(lblObservaciones);

            txtObservaciones = new TextBox();
            txtObservaciones.Font = new Font("Segoe UI", 10);
            txtObservaciones.Location = new Point(220, yPos);
            txtObservaciones.Size = new Size(630, 80);
            txtObservaciones.Multiline = true;
            this.Controls.Add(txtObservaciones);

            yPos += 110;

            Button btnConfirmar = new Button();
            btnConfirmar.Text = "Confirmar y Finalizar";
            btnConfirmar.Font = new Font("Segoe UI", 12);
            btnConfirmar.Location = new Point(250, yPos);
            btnConfirmar.Size = new Size(200, 50);
            btnConfirmar.BackColor = Color.LightGreen;
            btnConfirmar.Click += BtnConfirmar_Click;
            this.Controls.Add(btnConfirmar);

            Button btnDevolver = new Button();
            btnDevolver.Text = "Devolver a Cola";
            btnDevolver.Font = new Font("Segoe UI", 12);
            btnDevolver.Location = new Point(470, yPos);
            btnDevolver.Size = new Size(160, 50);
            btnDevolver.BackColor = Color.LightYellow;
            btnDevolver.Click += (s, e) =>
            {
                sistema.ColasPorHospital[medico.IdHospital].Enqueue(clave);
                sistema.GuardarTodosDatos();
                MessageBox.Show("Paciente devuelto a la cola", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };
            this.Controls.Add(btnDevolver);
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDiagnostico.Text))
                registro.Diagnostico = txtDiagnostico.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtTratamiento.Text))
                registro.Tratamiento = txtTratamiento.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtObservaciones.Text))
                registro.ObservacionDoctor = txtObservaciones.Text.Trim();

            registro.Confirmado = true;
            registro.IdMedico = medico.Id;

            if (!medico.PacientesAsignados.Contains(paciente.Id))
                medico.PacientesAsignados.Add(paciente.Id);

            sistema.GuardarTodosDatos();

            MessageBox.Show("Atención completada y guardada exitosamente", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}