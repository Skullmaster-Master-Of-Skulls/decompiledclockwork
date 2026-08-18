using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000005 RID: 5
	public class TestResultPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002560 File Offset: 0x00000760
		public override Type ComponentType
		{
			get
			{
				return typeof(Dictionary<string, object>);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000256C File Offset: 0x0000076C
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000256F File Offset: 0x0000076F
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000257B File Offset: 0x0000077B
		public TestResultPropertyDescriptor(string key) : base(key, null)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002585 File Offset: 0x00000785
		public override bool CanResetValue(object component)
		{
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002588 File Offset: 0x00000788
		public override object GetValue(object component)
		{
			DynamicClassFields dynamicClassFields = component as DynamicClassFields;
			if (dynamicClassFields != null && dynamicClassFields.ContainsKey(base.Name))
			{
				return dynamicClassFields[base.Name];
			}
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025BB File Offset: 0x000007BB
		public override void ResetValue(object component)
		{
			this.SetValue(component, string.Empty);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C9 File Offset: 0x000007C9
		public override void SetValue(object component, object value)
		{
			((DynamicClassFields)component)[base.Name] = value.ToString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000256C File Offset: 0x0000076C
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
