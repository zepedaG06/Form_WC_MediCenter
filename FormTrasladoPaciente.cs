using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MEDICENTER
{
    public class FormTrasladoPaciente : Form
    {
        private readonly Sistema sistema;
        private ComboBox cmbPaciente;
        private ComboBox cmbHospitalDestino;

        // Este formulario solo lo usa el personal hospitalario/medico.
        public FormTrasladoPaciente(Sistema sistemaParam)
        {
            sistema = sistemaParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(650, 360);
            this.Text = "Traslado de Paciente a Otro Hospital";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Icon = SystemIcons.Application;

            Label lblTitulo = new Label();
            lblTitulo.Text = "Traslado de paciente";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 20);
            lblTitulo.Size = new Size(650, 40);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            Label lblPaciente = new Label();
            lblPaciente.Text = "Seleccione el paciente a trasladar:";
            lblPaciente.Font = new Font("Segoe UI", 11);
            lblPaciente.Location = new Point(30, 80);
            lblPaciente.Size = new Size(580, 25);
            this.Controls.Add(lblPaciente);

            cmbPaciente = new ComboBox();
            cmbPaciente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaciente.Location = new Point(30, 110);
            cmbPaciente.Size = new Size(580, 28);
            cmbPaciente.Font = new Font("Segoe UI", 10);
            this.Controls.Add(cmbPaciente);

            foreach (var pac in sistema.Pacientes)
            {
                cmbPaciente.Items.Add(pac);
            }

            cmbPaciente.DisplayMember = nameof(Paciente.Nombre);

            Label lblDestino = new Label();
            lblDestino.Text = "Seleccione el hospital de destino:";
            lblDestino.Font = new Font("Segoe UI", 11);
            lblDestino.Location = new Point(30, 150);
            lblDestino.Size = new Size(580, 25);
            this.Controls.Add(lblDestino);

            cmbHospitalDestino = new ComboBox();
            cmbHospitalDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHospitalDestino.Location = new Point(30, 180);
            cmbHospitalDestino.Size = new Size(540, 28);
            cmbHospitalDestino.Font = new Font("Segoe UI", 10);
            this.Controls.Add(cmbHospitalDestino);

            // Cargar hospitales de destino
            foreach (var hosp in sistema.Hospitales)
            {
                cmbHospitalDestino.Items.Add(hosp);
            }

            Button btnTrasladar = new Button();
            btnTrasladar.Text = "Trasladar";
            btnTrasladar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnTrasladar.Location = new Point(170, 240);
            btnTrasladar.Size = new Size(130, 45);
            btnTrasladar.BackColor = Color.FromArgb(180, 220, 180);
            btnTrasladar.Click += (s, e) => EjecutarTraslado();
            this.Controls.Add(btnTrasladar);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 11);
            btnCancelar.Location = new Point(340, 240);
            btnCancelar.Size = new Size(130, 45);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void EjecutarTraslado()
        {
            if (cmbPaciente.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un paciente.", "Traslado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbHospitalDestino.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un hospital de destino.", "Traslado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var paciente = (Paciente)cmbPaciente.SelectedItem;

            if (paciente.Historial.Count == 0)
            {
                MessageBox.Show("El paciente no tiene registros medicos para trasladar.", "Traslado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ultimoRegistro = paciente.Historial.Last();
            string idHospitalOrigen = ultimoRegistro.IdHospital;
            var hospitalDestino = (Hospital)cmbHospitalDestino.SelectedItem;

            if (hospitalDestino.Id == idHospitalOrigen)
            {
                MessageBox.Show("El hospital de destino es el mismo que el hospital actual del ultimo registro.", "Traslado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var resultado = MessageBox.Show(
                $"Se trasladara el ultimo registro (ID: {ultimoRegistro.IdRegistro})\n" +
                $"del hospital {idHospitalOrigen} al hospital {hospitalDestino.Id}.\n\n" +
                "¿Desea continuar?",
                "Confirmar traslado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            // Mover el registro entre diccionarios de registros por hospital
            if (!string.IsNullOrEmpty(idHospitalOrigen) &&
                sistema.RegistrosPorHospital.ContainsKey(idHospitalOrigen))
            {
                sistema.RegistrosPorHospital[idHospitalOrigen].Remove(ultimoRegistro);
            }

            if (!sistema.RegistrosPorHospital.ContainsKey(hospitalDestino.Id))
            {
                sistema.RegistrosPorHospital[hospitalDestino.Id] = new System.Collections.Generic.List<RegistroMedico>();
            }

            ultimoRegistro.IdHospital = hospitalDestino.Id;
            sistema.RegistrosPorHospital[hospitalDestino.Id].Add(ultimoRegistro);

            // Registrar en lista de pacientes atendidos del hospital destino
            if (!hospitalDestino.PacientesAtendidos.Contains(paciente.Id))
            {
                hospitalDestino.PacientesAtendidos.Add(paciente.Id);
            }

            sistema.GuardarTodosDatos();

            MessageBox.Show("Traslado realizado correctamente.", "Traslado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}