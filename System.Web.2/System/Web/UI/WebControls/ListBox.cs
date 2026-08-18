using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000453 RID: 1107
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	public class ListBox : ListControl, IPostBackDataHandler
	{
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		// (set) Token: 0x06003564 RID: 13668 RVA: 0x0009E7E0 File Offset: 0x0009C9E0
		[Browsable(false)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x0009E7E9 File Offset: 0x0009C9E9
		// (set) Token: 0x06003566 RID: 13670 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
		[Browsable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x0009E7FA File Offset: 0x0009C9FA
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x0009E802 File Offset: 0x0009CA02
		[Browsable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000ACFD0 File Offset: 0x000AB1D0
		internal override bool IsMultiSelectInternal
		{
			get
			{
				return this.SelectionMode == ListSelectionMode.Multiple;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000ACFDC File Offset: 0x000AB1DC
		// (set) Token: 0x0600356B RID: 13675 RVA: 0x000AD005 File Offset: 0x000AB205
		[WebCategory("Appearance")]
		[DefaultValue(4)]
		[WebSysDescription("ListBox_Rows")]
		public virtual int Rows
		{
			get
			{
				object obj = this.ViewState["Rows"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 4;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Rows"] = value;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000AD02C File Offset: 0x000AB22C
		// (set) Token: 0x0600356D RID: 13677 RVA: 0x000AD055 File Offset: 0x000AB255
		[WebCategory("Behavior")]
		[DefaultValue(ListSelectionMode.Single)]
		[WebSysDescription("ListBox_SelectionMode")]
		public virtual ListSelectionMode SelectionMode
		{
			get
			{
				object obj = this.ViewState["SelectionMode"];
				if (obj != null)
				{
					return (ListSelectionMode)obj;
				}
				return ListSelectionMode.Single;
			}
			set
			{
				if (value < ListSelectionMode.Single || value > ListSelectionMode.Multiple)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000AD080 File Offset: 0x000AB280
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Rows.ToString(NumberFormatInfo.InvariantInfo));
			string uniqueID = this.UniqueID;
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000AD0C2 File Offset: 0x000AB2C2
		public virtual int[] GetSelectedIndices()
		{
			return (int[])this.SelectedIndicesInternal.ToArray(typeof(int));
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000AD0DE File Offset: 0x000AB2DE
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && this.SelectionMode == ListSelectionMode.Multiple && this.Enabled)
			{
				this.Page.RegisterRequiresPostBack(this);
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000AD10C File Offset: 0x000AB30C
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000AD118 File Offset: 0x000AB318
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			string[] values = postCollection.GetValues(postDataKey);
			bool flag = false;
			base.EnsureDataBoundInLoadPostData();
			if (values != null)
			{
				if (this.SelectionMode == ListSelectionMode.Single)
				{
					base.ValidateEvent(postDataKey, values[0]);
					int num = this.Items.FindByValueInternal(values[0], false);
					if (this.SelectedIndex != num)
					{
						base.SetPostDataSelection(num);
						flag = true;
					}
				}
				else
				{
					int num2 = values.Length;
					ArrayList selectedIndicesInternal = this.SelectedIndicesInternal;
					ArrayList arrayList = new ArrayList(num2);
					for (int i = 0; i < num2; i++)
					{
						base.ValidateEvent(postDataKey, values[i]);
						arrayList.Add(this.Items.FindByValueInternal(values[i], false));
					}
					int num3 = 0;
					if (selectedIndicesInternal != null)
					{
						num3 = selectedIndicesInternal.Count;
					}
					if (num3 == num2)
					{
						for (int j = 0; j < num2; j++)
						{
							if ((int)arrayList[j] != (int)selectedIndicesInternal[j])
							{
								flag = true;
								break;
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						base.SelectInternal(arrayList);
					}
				}
			}
			else if (this.SelectedIndex != -1)
			{
				base.SetPostDataSelection(-1);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000AD238 File Offset: 0x000AB438
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000AD240 File Offset: 0x000AB440
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && !this.Page.IsPostBackEventControlRegistered)
			{
				this.Page.AutoPostBackControl = this;
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}
	}
}
