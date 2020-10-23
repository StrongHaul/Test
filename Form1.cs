using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
	public partial class Form1 : Form
	{
		const int maxAttemps = 5;
		int currentAttempt = maxAttemps;
		int[] genNumber = new int[4];         // сгенерированное число

		public Form1()
		{
			InitializeComponent();
			label2.Text = currentAttempt.ToString();
		}

		private void button1_Click(object sender, EventArgs e)		// првоерить
		{
			if (textBox1.Text.Length < 4)
			{
				MessageBox.Show("Введите четырехзначное число!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			label3.Text = "";
			var userNumber = textBox1.Text.Select(digit => int.Parse(digit.ToString())).ToArray();     // число пользователя

			if (currentAttempt == maxAttemps)
			{
				Array.Copy(GenerateNumber(4, 10), genNumber, 4);
			}

			if (currentAttempt > 0)
			{
				if (NumberMatch(genNumber, userNumber))        // если числа совпадают
				{
					label3.Text = $"Угадали!\nЧисло обновлено.";
					currentAttempt = maxAttemps;
					return;
				}
				Compare(genNumber, userNumber);
				if (currentAttempt == 1)
				{
					label3.Text = "Вы израсходовали попытки.\nЧисло обновлено.";
					currentAttempt = maxAttemps;
					label2.Text = currentAttempt.ToString();
					return;
				}
			}
			currentAttempt--;
			label2.Text = currentAttempt.ToString();
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)      // ввод цифры
		{
			var number = e.KeyChar;
			if (textBox1.Text.Length < 4)
			{
				if (!char.IsDigit(number) && number != 8)   // если не число и не backspace
					e.Handled = true;       // пропустить обработку события
			}
			else
			{
				if (number != 8)
					e.Handled = true;
			}
		}

		public bool NumberMatch(int[] a, int[] b)		// проверка чисел на полное совпадение
		{
			bool match = true;
			for (int i = 0; i < 4; i++)
			{
				if (a[i] != b[i])
				{
					match = false;
					break;
				}
			}
			return match;
		}

		public void Compare(int[] genNumber, int[] userNumber)      // cравнение чисел
		{
			int k = 0;      // количество совпадающих цифр
			for (int i = 0; i < 4; i++)     // genNumber
			{
				for (int j = 0; j < 4; j++)     // userNumber
				{
					if (genNumber[i] == userNumber[j])
					{
						k++;
						if (i == j)
							label3.Text += $"BULL - {userNumber[j]}\n";
						else
							label3.Text += $"COW - {userNumber[j]}\n";
					}
				}
			}
			if (k == 0)
				label3.Text = "Совпадений не найдено";
		}

		public int[] GenerateNumber(int numLength, int maxDigit)        // генератор случайного числа
		{
			Random random = new Random();
			int digit, n = 0;
			var result = new int[4];

			while (n < numLength)
			{
				digit = random.Next(maxDigit);
				bool b = true;
				for (int i = 0; i < n; i++)
					if (digit == result[i])
					{
						b = false;
						break;
					}
				if (b)
				{
					result[n] = digit;
					n++;
				}
			}
			return result;
		}


	}
}
