using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F0 RID: 240
	internal sealed class ObjectDataSourceMethodEditor : UserControl
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0002F412 File Offset: 0x0002D612
		public ObjectDataSourceMethodEditor()
		{
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0002F428 File Offset: 0x0002D628
		public MethodInfo MethodInfo
		{
			get
			{
				ObjectDataSourceMethodEditor.MethodItem methodItem = this._methodComboBox.SelectedItem as ObjectDataSourceMethodEditor.MethodItem;
				if (methodItem == null)
				{
					return null;
				}
				return methodItem.MethodInfo;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0002F454 File Offset: 0x0002D654
		public Type DataObjectType
		{
			get
			{
				MethodInfo methodInfo = this.MethodInfo;
				if (methodInfo == null)
				{
					return null;
				}
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length != 1)
				{
					return null;
				}
				Type parameterType = parameters[0].ParameterType;
				if (ObjectDataSourceMethodEditor.IsPrimitiveType(parameterType))
				{
					return null;
				}
				return parameterType;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000860 RID: 2144 RVA: 0x0002F496 File Offset: 0x0002D696
		// (remove) Token: 0x06000861 RID: 2145 RVA: 0x0002F4A9 File Offset: 0x0002D6A9
		public event EventHandler MethodChanged
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceMethodEditor.EventMethodChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceMethodEditor.EventMethodChanged, value);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		private static void AppendGenericArguments(Type[] args, StringBuilder sb)
		{
			if (args.Length != 0)
			{
				sb.Append("<");
				for (int i = 0; i < args.Length; i++)
				{
					ObjectDataSourceMethodEditor.AppendTypeName(args[i], false, sb);
					if (i + 1 < args.Length)
					{
						sb.Append(", ");
					}
				}
				sb.Append(">");
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002F514 File Offset: 0x0002D714
		internal static void AppendTypeName(Type t, bool topLevelFullName, StringBuilder sb)
		{
			string text = topLevelFullName ? t.FullName : t.Name;
			if (t.IsGenericType)
			{
				int num = text.IndexOf("`", StringComparison.Ordinal);
				if (num == -1)
				{
					num = text.Length;
				}
				sb.Append(text.Substring(0, num));
				ObjectDataSourceMethodEditor.AppendGenericArguments(t.GetGenericArguments(), sb);
				if (num < text.Length)
				{
					num++;
					while (num < text.Length && char.IsNumber(text, num))
					{
						num++;
					}
					sb.Append(text.Substring(num));
					return;
				}
			}
			else
			{
				sb.Append(text);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002F5AC File Offset: 0x0002D7AC
		private bool FilterMethod(MethodInfo methodInfo, DataObjectMethodType methodType)
		{
			if (methodType == DataObjectMethodType.Select)
			{
				if (methodInfo.ReturnType == typeof(void))
				{
					return false;
				}
			}
			else
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters == null || parameters.Length == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		internal static string GetMethodSignature(MethodInfo mi)
		{
			if (mi == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(mi.Name);
			ObjectDataSourceMethodEditor.AppendGenericArguments(mi.GetGenericArguments(), stringBuilder);
			stringBuilder.Append("(");
			ParameterInfo[] parameters = mi.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				ObjectDataSourceMethodEditor.AppendTypeName(parameterInfo.ParameterType, false, stringBuilder);
				stringBuilder.Append(" " + parameterInfo.Name);
				if (parameterInfo.Position + 1 < parameters.Length)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(")");
			if (mi.ReturnType != typeof(void))
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				ObjectDataSourceMethodEditor.AppendTypeName(mi.ReturnType, false, stringBuilder2);
				return SR.GetString("ObjectDataSourceMethodEditor_SignatureFormat", new object[]
				{
					stringBuilder,
					stringBuilder2
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002F6E8 File Offset: 0x0002D8E8
		private void InitializeComponent()
		{
			this._helpLabel = new System.Windows.Forms.Label();
			this._methodLabel = new System.Windows.Forms.Label();
			this._signatureLabel = new System.Windows.Forms.Label();
			this._methodComboBox = new AutoSizeComboBox();
			this._signatureTextBox = new System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._helpLabel.Location = new Point(12, 12);
			this._helpLabel.Name = "_helpLabel";
			this._helpLabel.Size = new Size(487, 80);
			this._helpLabel.TabIndex = 10;
			this._methodLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._methodLabel.Location = new Point(12, 98);
			this._methodLabel.Name = "_methodLabel";
			this._methodLabel.Size = new Size(487, 16);
			this._methodLabel.TabIndex = 20;
			this._methodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._methodComboBox.Location = new Point(12, 116);
			this._methodComboBox.Name = "_methodComboBox";
			this._methodComboBox.Size = new Size(300, 21);
			this._methodComboBox.Sorted = true;
			this._methodComboBox.TabIndex = 30;
			this._methodComboBox.SelectedIndexChanged += this.OnMethodComboBoxSelectedIndexChanged;
			this._signatureLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._signatureLabel.Location = new Point(12, 145);
			this._signatureLabel.Name = "_signatureLabel";
			this._signatureLabel.Size = new Size(487, 16);
			this._signatureLabel.TabIndex = 40;
			this._signatureTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._signatureTextBox.BackColor = SystemColors.Control;
			this._signatureTextBox.Location = new Point(12, 163);
			this._signatureTextBox.Multiline = true;
			this._signatureTextBox.Name = "_signatureTextBox";
			this._signatureTextBox.ReadOnly = true;
			this._signatureTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._signatureTextBox.Size = new Size(487, 48);
			this._signatureTextBox.TabIndex = 50;
			this._signatureTextBox.Text = "";
			base.Controls.Add(this._signatureTextBox);
			base.Controls.Add(this._methodComboBox);
			base.Controls.Add(this._signatureLabel);
			base.Controls.Add(this._methodLabel);
			base.Controls.Add(this._helpLabel);
			base.Name = "ObjectDataSourceMethodEditor";
			base.Size = new Size(511, 220);
			base.ResumeLayout(false);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002F9C3 File Offset: 0x0002DBC3
		private void InitializeUI()
		{
			this._methodLabel.Text = SR.GetString("ObjectDataSourceMethodEditor_MethodLabel");
			this._signatureLabel.Text = SR.GetString("ObjectDataSource_General_MethodSignatureLabel");
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		private static bool IsPrimitiveType(Type t)
		{
			Type underlyingType = Nullable.GetUnderlyingType(t);
			if (underlyingType != null)
			{
				t = underlyingType;
			}
			return t.IsPrimitive || t == typeof(string) || t == typeof(DateTime) || t == typeof(decimal) || t == typeof(object);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002FA60 File Offset: 0x0002DC60
		private void OnMethodChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ObjectDataSourceMethodEditor.EventMethodChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002FA8E File Offset: 0x0002DC8E
		private void OnMethodComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnMethodChanged(EventArgs.Empty);
			this._signatureTextBox.Text = ObjectDataSourceMethodEditor.GetMethodSignature(this.MethodInfo);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002FAB4 File Offset: 0x0002DCB4
		public void SetMethodInformation(MethodInfo[] methods, string selectedMethodName, ParameterCollection selectedParameters, DataObjectMethodType methodType, Type dataObjectType)
		{
			try
			{
				this._signatureTextBox.Text = string.Empty;
				switch (methodType)
				{
				case DataObjectMethodType.Select:
					this._helpLabel.Text = SR.GetString("ObjectDataSourceMethodEditor_SelectHelpLabel");
					break;
				case DataObjectMethodType.Update:
					this._helpLabel.Text = SR.GetString("ObjectDataSourceMethodEditor_UpdateHelpLabel");
					break;
				case DataObjectMethodType.Insert:
					this._helpLabel.Text = SR.GetString("ObjectDataSourceMethodEditor_InsertHelpLabel");
					break;
				case DataObjectMethodType.Delete:
					this._helpLabel.Text = SR.GetString("ObjectDataSourceMethodEditor_DeleteHelpLabel");
					break;
				}
				this._methodComboBox.BeginUpdate();
				this._methodComboBox.Items.Clear();
				ObjectDataSourceMethodEditor.MethodItem selectedItem = null;
				bool flag = false;
				foreach (MethodInfo methodInfo in methods)
				{
					if (this.FilterMethod(methodInfo, methodType))
					{
						bool flag2 = false;
						DataObjectMethodAttribute dataObjectMethodAttribute = Attribute.GetCustomAttribute(methodInfo, typeof(DataObjectMethodAttribute), true) as DataObjectMethodAttribute;
						if (dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == methodType)
						{
							if (!flag)
							{
								this._methodComboBox.Items.Clear();
							}
							flag = true;
							flag2 = true;
						}
						else if (!flag)
						{
							flag2 = true;
						}
						bool flag3 = ObjectDataSourceDesigner.IsMatchingMethod(methodInfo, selectedMethodName, selectedParameters, dataObjectType);
						if (flag2 || flag3)
						{
							ObjectDataSourceMethodEditor.MethodItem methodItem = new ObjectDataSourceMethodEditor.MethodItem(methodInfo);
							this._methodComboBox.Items.Add(methodItem);
							if (flag3)
							{
								selectedItem = methodItem;
							}
							else if (dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == methodType && dataObjectMethodAttribute.IsDefault && selectedMethodName.Length == 0)
							{
								selectedItem = methodItem;
							}
						}
					}
				}
				if (methodType != DataObjectMethodType.Select)
				{
					this._methodComboBox.Items.Insert(0, new ObjectDataSourceMethodEditor.MethodItem(null));
				}
				this._methodComboBox.InvalidateDropDownWidth();
				this._methodComboBox.SelectedItem = selectedItem;
			}
			finally
			{
				this._methodComboBox.EndUpdate();
			}
		}

		// Token: 0x040004E7 RID: 1255
		private static readonly object EventMethodChanged = new object();

		// Token: 0x040004E8 RID: 1256
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x040004E9 RID: 1257
		private System.Windows.Forms.Label _methodLabel;

		// Token: 0x040004EA RID: 1258
		private AutoSizeComboBox _methodComboBox;

		// Token: 0x040004EB RID: 1259
		private System.Windows.Forms.Label _signatureLabel;

		// Token: 0x040004EC RID: 1260
		private System.Windows.Forms.TextBox _signatureTextBox;

		// Token: 0x02000416 RID: 1046
		private sealed class MethodItem
		{
			// Token: 0x06002816 RID: 10262 RVA: 0x000F4FED File Offset: 0x000F31ED
			public MethodItem(MethodInfo methodInfo)
			{
				this._methodInfo = methodInfo;
			}

			// Token: 0x17000866 RID: 2150
			// (get) Token: 0x06002817 RID: 10263 RVA: 0x000F4FFC File Offset: 0x000F31FC
			public MethodInfo MethodInfo
			{
				get
				{
					return this._methodInfo;
				}
			}

			// Token: 0x06002818 RID: 10264 RVA: 0x000F5004 File Offset: 0x000F3204
			public override string ToString()
			{
				if (this._methodInfo == null)
				{
					return SR.GetString("ObjectDataSourceMethodEditor_NoMethod");
				}
				return ObjectDataSourceMethodEditor.GetMethodSignature(this._methodInfo);
			}

			// Token: 0x04001C8F RID: 7311
			private MethodInfo _methodInfo;
		}
	}
}
