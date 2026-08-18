using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D8 RID: 984
	[__DynamicallyInvokable]
	public class OperationDescriptionCollection : Collection<OperationDescription>
	{
		// Token: 0x06002516 RID: 9494 RVA: 0x00084FDA File Offset: 0x000831DA
		internal OperationDescriptionCollection()
		{
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00084FE4 File Offset: 0x000831E4
		[__DynamicallyInvokable]
		public OperationDescription Find(string name)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Name == name)
				{
					return base[i];
				}
			}
			return null;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00085020 File Offset: 0x00083220
		[__DynamicallyInvokable]
		public Collection<OperationDescription> FindAll(string name)
		{
			Collection<OperationDescription> collection = new Collection<OperationDescription>();
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Name == name)
				{
					collection.Add(base[i]);
				}
			}
			return collection;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00085066 File Offset: 0x00083266
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, OperationDescription item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00085083 File Offset: 0x00083283
		[__DynamicallyInvokable]
		protected override void SetItem(int index, OperationDescription item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
