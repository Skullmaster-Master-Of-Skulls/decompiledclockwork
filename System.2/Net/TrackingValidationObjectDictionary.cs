using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000229 RID: 553
	internal class TrackingValidationObjectDictionary : StringDictionary
	{
		// Token: 0x06001469 RID: 5225 RVA: 0x0006BCF3 File Offset: 0x00069EF3
		internal TrackingValidationObjectDictionary(IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators)
		{
			this.IsChanged = false;
			this.validators = validators;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0006BD0C File Offset: 0x00069F0C
		private void PersistValue(string key, string value, bool addValue)
		{
			key = key.ToLowerInvariant();
			if (!string.IsNullOrEmpty(value))
			{
				if (this.validators != null && this.validators.ContainsKey(key))
				{
					object obj = this.validators[key](value);
					if (this.internalObjects == null)
					{
						this.internalObjects = new Dictionary<string, object>();
					}
					if (addValue)
					{
						this.internalObjects.Add(key, obj);
						base.Add(key, obj.ToString());
					}
					else
					{
						this.internalObjects[key] = obj;
						base[key] = obj.ToString();
					}
				}
				else if (addValue)
				{
					base.Add(key, value);
				}
				else
				{
					base[key] = value;
				}
				this.IsChanged = true;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0006BDBF File Offset: 0x00069FBF
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x0006BDC7 File Offset: 0x00069FC7
		internal bool IsChanged { get; set; }

		// Token: 0x0600146D RID: 5229 RVA: 0x0006BDD0 File Offset: 0x00069FD0
		internal object InternalGet(string key)
		{
			if (this.internalObjects != null && this.internalObjects.ContainsKey(key))
			{
				return this.internalObjects[key];
			}
			return base[key];
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0006BDFC File Offset: 0x00069FFC
		internal void InternalSet(string key, object value)
		{
			if (this.internalObjects == null)
			{
				this.internalObjects = new Dictionary<string, object>();
			}
			this.internalObjects[key] = value;
			base[key] = value.ToString();
			this.IsChanged = true;
		}

		// Token: 0x17000448 RID: 1096
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				this.PersistValue(key, value, false);
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0006BE46 File Offset: 0x0006A046
		public override void Add(string key, string value)
		{
			this.PersistValue(key, value, true);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0006BE51 File Offset: 0x0006A051
		public override void Clear()
		{
			if (this.internalObjects != null)
			{
				this.internalObjects.Clear();
			}
			base.Clear();
			this.IsChanged = true;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0006BE73 File Offset: 0x0006A073
		public override void Remove(string key)
		{
			if (this.internalObjects != null && this.internalObjects.ContainsKey(key))
			{
				this.internalObjects.Remove(key);
			}
			base.Remove(key);
			this.IsChanged = true;
		}

		// Token: 0x04001631 RID: 5681
		private IDictionary<string, object> internalObjects;

		// Token: 0x04001632 RID: 5682
		private readonly IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators;

		// Token: 0x0200076A RID: 1898
		// (Invoke) Token: 0x0600425C RID: 16988
		internal delegate object ValidateAndParseValue(object valueToValidate);
	}
}
