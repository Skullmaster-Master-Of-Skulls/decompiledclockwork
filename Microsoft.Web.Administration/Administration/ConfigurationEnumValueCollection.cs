using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000025 RID: 37
	[DebuggerDisplay("Count = {Count}")]
	public sealed class ConfigurationEnumValueCollection : ICollection, IEnumerable<ConfigurationEnumValue>, IEnumerable
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00005FD8 File Offset: 0x00004FD8
		internal ConfigurationEnumValueCollection(IAppHostConstantValueCollection enumValues)
		{
			this._values = new List<ConfigurationEnumValue>((int)enumValues.Count);
			for (uint num = 0U; num < enumValues.Count; num += 1U)
			{
				IAppHostConstantValue value = enumValues[num];
				ConfigurationEnumValue item = new ConfigurationEnumValue(value);
				this._values.Add(item);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000602D File Offset: 0x0000502D
		public int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x170000C3 RID: 195
		public ConfigurationEnumValue this[int index]
		{
			get
			{
				return this._values[index];
			}
		}

		// Token: 0x170000C4 RID: 196
		public ConfigurationEnumValue this[string name]
		{
			get
			{
				foreach (ConfigurationEnumValue configurationEnumValue in this._values)
				{
					if (string.Equals(configurationEnumValue.Name, name, StringComparison.OrdinalIgnoreCase))
					{
						return configurationEnumValue;
					}
				}
				return null;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000060AC File Offset: 0x000050AC
		public IEnumerator<ConfigurationEnumValue> GetEnumerator()
		{
			return this._values.GetEnumerator();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000060C0 File Offset: 0x000050C0
		public string GetName(long value)
		{
			foreach (ConfigurationEnumValue configurationEnumValue in this)
			{
				if (configurationEnumValue != null && configurationEnumValue.Value == value)
				{
					return configurationEnumValue.Name;
				}
			}
			return null;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000611C File Offset: 0x0000511C
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._values).CopyTo(array, index);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000612B File Offset: 0x0000512B
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._values).IsSynchronized;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006138 File Offset: 0x00005138
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._values).SyncRoot;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006145 File Offset: 0x00005145
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000067 RID: 103
		private List<ConfigurationEnumValue> _values;
	}
}
