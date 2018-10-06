using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lockscreen_Layout
{
	public partial class MainForm : Form
	{
		private string registryPath = @"Software\Microsoft\Windows\CurrentVersion\Lock Screen";
		private string valueName = "SlideshowLayout";

		private string lockscreenLayout;

		public MainForm()
		{
			InitializeComponent();

			Text = Application.ProductName;

			lockscreenLayout = GetLockscreenLayout();

			var radioButtonName = "radioButton" + lockscreenLayout;
			var radioButton = Controls.Find(radioButtonName, false).First() as RadioButton;
			if (radioButton != null)
			{
				radioButton.Checked = true;
			}
		}

		private string GetLockscreenLayout()
		{
			var key = Registry.CurrentUser.OpenSubKey(registryPath);
			var value = key.GetValue(valueName);

			return value.ToString();
		}

		private void SetLockscreenLayout(int value)
		{
			var key = Registry.CurrentUser.OpenSubKey(registryPath, true);
			key.SetValue(valueName, value);
		}

		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			var radio = (sender as RadioButton);
			var value = radio.Name.Substring(radio.Name.Length - 1);
			SetLockscreenLayout(int.Parse(value));
		}
	}
}
