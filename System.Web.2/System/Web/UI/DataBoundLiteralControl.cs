using System;
using System.ComponentModel;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x02000273 RID: 627
	[ToolboxItem(false)]
	public sealed class DataBoundLiteralControl : Control, ITextControl
	{
		// Token: 0x06001DCE RID: 7630 RVA: 0x00060A81 File Offset: 0x0005EC81
		public DataBoundLiteralControl(int staticLiteralsCount, int dataBoundLiteralCount)
		{
			this._staticLiterals = new string[staticLiteralsCount];
			this._dataBoundLiteral = new string[dataBoundLiteralCount];
			base.PreventAutoID();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00060AA7 File Offset: 0x0005ECA7
		public void SetStaticString(int index, string s)
		{
			this._staticLiterals[index] = s;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00060AB2 File Offset: 0x0005ECB2
		public void SetDataBoundString(int index, string s)
		{
			this._dataBoundLiteral[index] = s;
			this._hasDataBoundStrings = true;
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x00060AC4 File Offset: 0x0005ECC4
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = this._dataBoundLiteral.Length;
				for (int i = 0; i < this._staticLiterals.Length; i++)
				{
					if (this._staticLiterals[i] != null)
					{
						stringBuilder.Append(this._staticLiterals[i]);
					}
					if (i < num && this._dataBoundLiteral[i] != null)
					{
						stringBuilder.Append(this._dataBoundLiteral[i]);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00060B37 File Offset: 0x0005ED37
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this._dataBoundLiteral = (string[])savedState;
				this._hasDataBoundStrings = true;
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00060B4F File Offset: 0x0005ED4F
		protected override object SaveViewState()
		{
			if (!this._hasDataBoundStrings)
			{
				return null;
			}
			return this._dataBoundLiteral;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00060B64 File Offset: 0x0005ED64
		protected internal override void Render(HtmlTextWriter output)
		{
			int num = this._dataBoundLiteral.Length;
			for (int i = 0; i < this._staticLiterals.Length; i++)
			{
				if (this._staticLiterals[i] != null)
				{
					output.Write(this._staticLiterals[i]);
				}
				if (i < num && this._dataBoundLiteral[i] != null)
				{
					output.Write(this._dataBoundLiteral[i]);
				}
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00060BC1 File Offset: 0x0005EDC1
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x00010D64 File Offset: 0x0000EF64
		string ITextControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400196B RID: 6507
		private string[] _staticLiterals;

		// Token: 0x0400196C RID: 6508
		private string[] _dataBoundLiteral;

		// Token: 0x0400196D RID: 6509
		private bool _hasDataBoundStrings;
	}
}
