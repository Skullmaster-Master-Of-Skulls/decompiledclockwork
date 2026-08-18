using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
	// Token: 0x02000403 RID: 1027
	internal class ToolStripSettingsManager
	{
		// Token: 0x060046DD RID: 18141 RVA: 0x0012905E File Offset: 0x0012725E
		internal ToolStripSettingsManager(Form owner, string formKey)
		{
			this.form = owner;
			this.formKey = formKey;
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00129074 File Offset: 0x00127274
		internal void Load()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.FindToolStrips(true, this.form.Controls))
			{
				ToolStrip toolStrip = (ToolStrip)obj;
				if (toolStrip != null && !string.IsNullOrEmpty(toolStrip.Name))
				{
					ToolStripSettings toolStripSettings = new ToolStripSettings(this.GetSettingsKey(toolStrip));
					if (!toolStripSettings.IsDefault)
					{
						arrayList.Add(new ToolStripSettingsManager.SettingsStub(toolStripSettings));
					}
				}
			}
			this.ApplySettings(arrayList);
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x0012911C File Offset: 0x0012731C
		internal void Save()
		{
			foreach (object obj in this.FindToolStrips(true, this.form.Controls))
			{
				ToolStrip toolStrip = (ToolStrip)obj;
				if (toolStrip != null && !string.IsNullOrEmpty(toolStrip.Name))
				{
					ToolStripSettings toolStripSettings = new ToolStripSettings(this.GetSettingsKey(toolStrip));
					ToolStripSettingsManager.SettingsStub settingsStub = new ToolStripSettingsManager.SettingsStub(toolStrip);
					toolStripSettings.ItemOrder = settingsStub.ItemOrder;
					toolStripSettings.Name = settingsStub.Name;
					toolStripSettings.Location = settingsStub.Location;
					toolStripSettings.Size = settingsStub.Size;
					toolStripSettings.ToolStripPanelName = settingsStub.ToolStripPanelName;
					toolStripSettings.Visible = settingsStub.Visible;
					toolStripSettings.Save();
				}
			}
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x001291F4 File Offset: 0x001273F4
		internal static string GetItemOrder(ToolStrip toolStrip)
		{
			StringBuilder stringBuilder = new StringBuilder(toolStrip.Items.Count);
			for (int i = 0; i < toolStrip.Items.Count; i++)
			{
				stringBuilder.Append((toolStrip.Items[i].Name == null) ? "null" : toolStrip.Items[i].Name);
				if (i != toolStrip.Items.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x0012927C File Offset: 0x0012747C
		private void ApplySettings(ArrayList toolStripSettingsToApply)
		{
			if (toolStripSettingsToApply.Count == 0)
			{
				return;
			}
			this.SuspendAllLayout(this.form);
			Dictionary<string, ToolStrip> itemLocationHash = this.BuildItemOriginationHash();
			Dictionary<object, List<ToolStripSettingsManager.SettingsStub>> dictionary = new Dictionary<object, List<ToolStripSettingsManager.SettingsStub>>();
			foreach (object obj in toolStripSettingsToApply)
			{
				ToolStripSettingsManager.SettingsStub settingsStub = (ToolStripSettingsManager.SettingsStub)obj;
				object obj2 = (!string.IsNullOrEmpty(settingsStub.ToolStripPanelName)) ? settingsStub.ToolStripPanelName : null;
				if (obj2 == null)
				{
					if (!string.IsNullOrEmpty(settingsStub.Name))
					{
						ToolStrip toolStrip = ToolStripManager.FindToolStrip(this.form, settingsStub.Name);
						this.ApplyToolStripSettings(toolStrip, settingsStub, itemLocationHash);
					}
				}
				else
				{
					if (!dictionary.ContainsKey(obj2))
					{
						dictionary[obj2] = new List<ToolStripSettingsManager.SettingsStub>();
					}
					dictionary[obj2].Add(settingsStub);
				}
			}
			ArrayList arrayList = this.FindToolStripPanels(true, this.form.Controls);
			foreach (object obj3 in arrayList)
			{
				ToolStripPanel toolStripPanel = (ToolStripPanel)obj3;
				foreach (object obj4 in toolStripPanel.Controls)
				{
					Control control = (Control)obj4;
					control.Visible = false;
				}
				string text = toolStripPanel.Name;
				if (string.IsNullOrEmpty(text) && toolStripPanel.Parent is ToolStripContainer && !string.IsNullOrEmpty(toolStripPanel.Parent.Name))
				{
					text = toolStripPanel.Parent.Name + "." + toolStripPanel.Dock.ToString();
				}
				toolStripPanel.BeginInit();
				if (dictionary.ContainsKey(text))
				{
					List<ToolStripSettingsManager.SettingsStub> list = dictionary[text];
					if (list != null)
					{
						foreach (ToolStripSettingsManager.SettingsStub settingsStub2 in list)
						{
							if (!string.IsNullOrEmpty(settingsStub2.Name))
							{
								ToolStrip toolStrip2 = ToolStripManager.FindToolStrip(this.form, settingsStub2.Name);
								this.ApplyToolStripSettings(toolStrip2, settingsStub2, itemLocationHash);
								toolStripPanel.Join(toolStrip2, settingsStub2.Location);
							}
						}
					}
				}
				toolStripPanel.EndInit();
			}
			this.ResumeAllLayout(this.form, true);
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x00129554 File Offset: 0x00127754
		private void ApplyToolStripSettings(ToolStrip toolStrip, ToolStripSettingsManager.SettingsStub settings, Dictionary<string, ToolStrip> itemLocationHash)
		{
			if (toolStrip != null)
			{
				toolStrip.Visible = settings.Visible;
				toolStrip.Size = settings.Size;
				string itemOrder = settings.ItemOrder;
				if (!string.IsNullOrEmpty(itemOrder))
				{
					string[] array = itemOrder.Split(new char[]
					{
						','
					});
					Regex regex = new Regex("(\\S+)");
					int num = 0;
					while (num < toolStrip.Items.Count && num < array.Length)
					{
						Match match = regex.Match(array[num]);
						if (match != null && match.Success)
						{
							string value = match.Value;
							if (!string.IsNullOrEmpty(value) && itemLocationHash.ContainsKey(value))
							{
								toolStrip.Items.Insert(num, itemLocationHash[value].Items[value]);
							}
						}
						num++;
					}
				}
			}
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x00129620 File Offset: 0x00127820
		private Dictionary<string, ToolStrip> BuildItemOriginationHash()
		{
			ArrayList arrayList = this.FindToolStrips(true, this.form.Controls);
			Dictionary<string, ToolStrip> dictionary = new Dictionary<string, ToolStrip>();
			if (arrayList != null)
			{
				foreach (object obj in arrayList)
				{
					ToolStrip toolStrip = (ToolStrip)obj;
					foreach (object obj2 in toolStrip.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj2;
						if (!string.IsNullOrEmpty(toolStripItem.Name))
						{
							dictionary[toolStripItem.Name] = toolStrip;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001296F8 File Offset: 0x001278F8
		private ArrayList FindControls(Type baseType, bool searchAllChildren, Control.ControlCollection controlsToLookIn, ArrayList foundControls)
		{
			if (controlsToLookIn == null || foundControls == null)
			{
				return null;
			}
			try
			{
				for (int i = 0; i < controlsToLookIn.Count; i++)
				{
					if (controlsToLookIn[i] != null && baseType.IsAssignableFrom(controlsToLookIn[i].GetType()))
					{
						foundControls.Add(controlsToLookIn[i]);
					}
				}
				if (searchAllChildren)
				{
					for (int j = 0; j < controlsToLookIn.Count; j++)
					{
						if (controlsToLookIn[j] != null && !(controlsToLookIn[j] is Form) && controlsToLookIn[j].Controls != null && controlsToLookIn[j].Controls.Count > 0)
						{
							foundControls = this.FindControls(baseType, searchAllChildren, controlsToLookIn[j].Controls, foundControls);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return foundControls;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x001297D4 File Offset: 0x001279D4
		private ArrayList FindToolStripPanels(bool searchAllChildren, Control.ControlCollection controlsToLookIn)
		{
			return this.FindControls(typeof(ToolStripPanel), true, this.form.Controls, new ArrayList());
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x001297F7 File Offset: 0x001279F7
		private ArrayList FindToolStrips(bool searchAllChildren, Control.ControlCollection controlsToLookIn)
		{
			return this.FindControls(typeof(ToolStrip), true, this.form.Controls, new ArrayList());
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x0012981A File Offset: 0x00127A1A
		private string GetSettingsKey(ToolStrip toolStrip)
		{
			if (toolStrip != null)
			{
				return this.formKey + "." + toolStrip.Name;
			}
			return string.Empty;
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x0012983C File Offset: 0x00127A3C
		private void ResumeAllLayout(Control start, bool performLayout)
		{
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.ResumeAllLayout(controls[i], performLayout);
			}
			start.ResumeLayout(performLayout);
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x00129878 File Offset: 0x00127A78
		private void SuspendAllLayout(Control start)
		{
			start.SuspendLayout();
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.SuspendAllLayout(controls[i]);
			}
		}

		// Token: 0x040026BE RID: 9918
		private Form form;

		// Token: 0x040026BF RID: 9919
		private string formKey;

		// Token: 0x0200081B RID: 2075
		private struct SettingsStub
		{
			// Token: 0x06006FD2 RID: 28626 RVA: 0x0019AE7C File Offset: 0x0019907C
			public SettingsStub(ToolStrip toolStrip)
			{
				this.ToolStripPanelName = string.Empty;
				ToolStripPanel toolStripPanel = toolStrip.Parent as ToolStripPanel;
				if (toolStripPanel != null)
				{
					if (!string.IsNullOrEmpty(toolStripPanel.Name))
					{
						this.ToolStripPanelName = toolStripPanel.Name;
					}
					else if (toolStripPanel.Parent is ToolStripContainer && !string.IsNullOrEmpty(toolStripPanel.Parent.Name))
					{
						this.ToolStripPanelName = toolStripPanel.Parent.Name + "." + toolStripPanel.Dock.ToString();
					}
				}
				this.Visible = toolStrip.Visible;
				this.Size = toolStrip.Size;
				this.Location = toolStrip.Location;
				this.Name = toolStrip.Name;
				this.ItemOrder = ToolStripSettingsManager.GetItemOrder(toolStrip);
			}

			// Token: 0x06006FD3 RID: 28627 RVA: 0x0019AF48 File Offset: 0x00199148
			public SettingsStub(ToolStripSettings toolStripSettings)
			{
				this.ToolStripPanelName = toolStripSettings.ToolStripPanelName;
				this.Visible = toolStripSettings.Visible;
				this.Size = toolStripSettings.Size;
				this.Location = toolStripSettings.Location;
				this.Name = toolStripSettings.Name;
				this.ItemOrder = toolStripSettings.ItemOrder;
			}

			// Token: 0x0400432C RID: 17196
			public bool Visible;

			// Token: 0x0400432D RID: 17197
			public string ToolStripPanelName;

			// Token: 0x0400432E RID: 17198
			public Point Location;

			// Token: 0x0400432F RID: 17199
			public Size Size;

			// Token: 0x04004330 RID: 17200
			public string ItemOrder;

			// Token: 0x04004331 RID: 17201
			public string Name;
		}
	}
}
