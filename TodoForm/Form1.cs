﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TodoForm
{
    public partial class frmKezdo : Form
    {
        private List<string> todo = new List<string>();

        public frmKezdo()
        {
            InitializeComponent();
            StreamReader be = new StreamReader("todoitems.txt");
            while (!be.EndOfStream)
            {
                todo.Add(be.ReadLine());
            }
            be.Close();
        }

        private void mKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmKezdo_Shown(object sender, EventArgs e)
        {
            tbBevitel.Text = "";
            lbTodo.Items.Clear();
            tbBevitel.Focus();
        }

        private void btnListabol_Click(object sender, EventArgs e)
        {
            frmLista formLista = new frmLista(todo);

            var result = formLista.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbBevitel.Text = formLista.SelectedTodo;
            }

            tbBevitel.Focus();
            tbBevitel.SelectionStart = tbBevitel.Text.Length;
            tbBevitel.SelectionLength = 0;

            tbBevitel.Text = formLista.SelectedTodo;
        }

        private void btnFelvitel_Click(object sender, EventArgs e)
        {
            string todo = tbBevitel.Text.Trim();
            if (todo != "" && !(lbTodo.Items.Contains(todo)))
            {
                lbTodo.Items.Add(todo);
                tbBevitel.Text = "";
            }
        }

        private void btnEltavolit_Click(object sender, EventArgs e)
        {
            lbTodo.Items.Clear();
        }

        private void btnKivesz_Click(object sender, EventArgs e)
        {
            if (lbTodo.SelectedIndex == -1)
            {
                MessageBox.Show("Válassz ki valamit","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                lbTodo.Items.Remove(lbTodo.SelectedItem);
            }
        }

        private void mBeolvasas_Click(object sender, EventArgs e)
        {
            if (ofdMegnyitas.ShowDialog() == DialogResult.OK)
            {
                lbTodo.Items.Clear();

                StreamReader be = new StreamReader(ofdMegnyitas.FileName);

                while (!be.EndOfStream)
                {
                    lbTodo.Items.Add(be.ReadLine());
                }
                be.Close();
                MessageBox.Show("Sikeres beolvasás", "Beolvasás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mMentes_Click(object sender, EventArgs e)
        {

            if (lbTodo.Items.Count > 0)
            {
                sfdMentes.FileName = FileNameDate();
                if (sfdMentes.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter ki = new StreamWriter(sfdMentes.FileName);

                    foreach (var item in lbTodo.Items)
                    {
                        ki.WriteLine(item);
                    }
                    ki.Close();
                    MessageBox.Show("Sikeres mentés", "Mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Nincs mit menteni", "Üres lista", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private string FileNameDate()
        {
            var datum = DateTime.Now;
            string ev = datum.Year.ToString();
            string honap = "";

            if (datum.Month < 10)
            {
                honap = "0" + datum.Month.ToString();
            }
            else
            {
                honap = datum.Month.ToString();
            }

            string nap = "";

            if (datum.Day < 10)
            {
                nap = "0" + datum.Day.ToString();
            }
            else
            {
                nap = datum.Day.ToString();
            }

            return ev + honap + nap;
        }
    }
}
