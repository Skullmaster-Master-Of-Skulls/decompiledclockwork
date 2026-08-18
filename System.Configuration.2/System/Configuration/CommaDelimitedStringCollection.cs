using System;
using System.Collections.Specialized;
using System.Text;

namespace System.Configuration
{
	// Token: 0x0200008F RID: 143
	public sealed class CommaDelimitedStringCollection : StringCollection
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0001C632 File Offset: 0x0001A832
		public CommaDelimitedStringCollection()
		{
			this._ReadOnly = false;
			this._Modified = false;
			this._OriginalString = this.ToString();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001C654 File Offset: 0x0001A854
		internal void FromString(string list)
		{
			char[] separator = new char[]
			{
				','
			};
			if (list != null)
			{
				string[] array = list.Split(separator);
				foreach (string text in array)
				{
					string text2 = text.Trim();
					if (text2.Length != 0)
					{
						this.Add(text.Trim());
					}
				}
			}
			this._OriginalString = this.ToString();
			this._ReadOnly = false;
			this._Modified = false;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public override string ToString()
		{
			string text = null;
			if (base.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text2 in this)
				{
					this.ThrowIfContainsDelimiter(text2);
					stringBuilder.Append(text2.Trim());
					stringBuilder.Append(',');
				}
				text = stringBuilder.ToString();
				if (text.Length > 0)
				{
					text = text.Substring(0, text.Length - 1);
				}
				if (text.Length == 0)
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001C76C File Offset: 0x0001A96C
		private void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001C786 File Offset: 0x0001A986
		private void ThrowIfContainsDelimiter(string value)
		{
			if (value.Contains(","))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_value_cannot_contain", new object[]
				{
					","
				}));
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001C7B3 File Offset: 0x0001A9B3
		public void SetReadOnly()
		{
			this._ReadOnly = true;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		public bool IsModified
		{
			get
			{
				return this._Modified || this.ToString() != this._OriginalString;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001C7D9 File Offset: 0x0001A9D9
		public new bool IsReadOnly
		{
			get
			{
				return this._ReadOnly;
			}
		}

		// Token: 0x170001E2 RID: 482
		public new string this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				this.ThrowIfReadOnly();
				this.ThrowIfContainsDelimiter(value);
				this._Modified = true;
				base[index] = value.Trim();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C80D File Offset: 0x0001AA0D
		public new void Add(string value)
		{
			this.ThrowIfReadOnly();
			this.ThrowIfContainsDelimiter(value);
			this._Modified = true;
			base.Add(value.Trim());
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001C830 File Offset: 0x0001AA30
		public new void AddRange(string[] range)
		{
			this.ThrowIfReadOnly();
			this._Modified = true;
			foreach (string text in range)
			{
				this.ThrowIfContainsDelimiter(text);
				base.Add(text.Trim());
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001C872 File Offset: 0x0001AA72
		public new void Clear()
		{
			this.ThrowIfReadOnly();
			this._Modified = true;
			base.Clear();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001C887 File Offset: 0x0001AA87
		public new void Insert(int index, string value)
		{
			this.ThrowIfReadOnly();
			this.ThrowIfContainsDelimiter(value);
			this._Modified = true;
			base.Insert(index, value.Trim());
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C8AA File Offset: 0x0001AAAA
		public new void Remove(string value)
		{
			this.ThrowIfReadOnly();
			this.ThrowIfContainsDelimiter(value);
			this._Modified = true;
			base.Remove(value.Trim());
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C8CC File Offset: 0x0001AACC
		public CommaDelimitedStringCollection Clone()
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			foreach (string value in this)
			{
				commaDelimitedStringCollection.Add(value);
			}
			commaDelimitedStringCollection._Modified = false;
			commaDelimitedStringCollection._ReadOnly = this._ReadOnly;
			commaDelimitedStringCollection._OriginalString = this._OriginalString;
			return commaDelimitedStringCollection;
		}

		// Token: 0x0400034B RID: 843
		private bool _Modified;

		// Token: 0x0400034C RID: 844
		private bool _ReadOnly;

		// Token: 0x0400034D RID: 845
		private string _OriginalString;
	}
}
