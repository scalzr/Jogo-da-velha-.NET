using System;
using System.Windows.Forms;

namespace JogoDaVelha
{
    public partial class Form1 : Form
    {
        private bool turno = true; // true = turno do X; false = turno do O
        private int contadorTurnos = 0; // Contador de turnos
        private const int tamanhoGrid = 3; // Tamanho da grade do jogo da velha
        private string[,] grid = new string[tamanhoGrid, tamanhoGrid]; // Matriz para armazenar o estado do jogo

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender; // Obter o botão que foi clicado
            int linha = button.TabIndex / tamanhoGrid; // Calcular a linha com base no índice
            int coluna = button.TabIndex % tamanhoGrid; // Calcular a coluna com base no índice

            if (turno)
            {
                button.Text = "X"; // Definir o texto do botão para "X"
                grid[linha, coluna] = "X"; // Atualizar a matriz do jogo
            }
            else
            {
                button.Text = "O"; // Definir o texto do botão para "O"
                grid[linha, coluna] = "O"; // Atualizar a matriz do jogo
            }

            button.Enabled = false; // Desabilitar o botão após ser clicado
            contadorTurnos++; // Incrementar o contador de turnos
            turno = !turno; // Alternar o turno

            VerificarVencedor(); // Verificar se há um vencedor
        }

        private void VerificarVencedor()
        {
            string vencedor = null;

            // Verificar linhas
            for (int i = 0; i < tamanhoGrid; i++)
            {
                if ((grid[i, 0] == grid[i, 1]) && (grid[i, 1] == grid[i, 2]) && (grid[i, 0] != null))
                {
                    vencedor = grid[i, 0];
                }
            }

            // Verificar colunas
            for (int i = 0; i < tamanhoGrid; i++)
            {
                if ((grid[0, i] == grid[1, i]) && (grid[1, i] == grid[2, i]) && (grid[0, i] != null))
                {
                    vencedor = grid[0, i];
                }
            }

            // Verificar diagonais
            if ((grid[0, 0] == grid[1, 1]) && (grid[1, 1] == grid[2, 2]) && (grid[0, 0] != null))
            {
                vencedor = grid[0, 0];
            }
            if ((grid[0, 2] == grid[1, 1]) && (grid[1, 1] == grid[2, 0]) && (grid[0, 2] != null))
            {
                vencedor = grid[0, 2];
            }

            if (vencedor != null)
            {
                labelStatus.Text = "Vencedor: " + vencedor;
                DesabilitarBotoes();
            }
            else if (contadorTurnos == 9)
            {
                labelStatus.Text = "Empate!";
            }
        }

        private void DesabilitarBotoes()
        {
            foreach (Control c in Controls)
            {
                Button button = c as Button;
                if (button != null && button.Name.StartsWith("button") && button.Name != "buttonReset")
                {
                    button.Enabled = false;
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            turno = true;
            contadorTurnos = 0;
            labelStatus.Text = "Bem-vindo ao Jogo da Velha!";
            grid = new string[tamanhoGrid, tamanhoGrid];

            foreach (Control c in Controls)
            {
                Button button = c as Button;
                if (button != null && button.Name.StartsWith("button") && button.Name != "buttonReset")
                {
                    button.Enabled = true;
                    button.Text = "";
                }
            }
        }
    }
}
