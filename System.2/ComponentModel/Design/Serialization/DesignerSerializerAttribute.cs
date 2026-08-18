using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000607 RID: 1543
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		// Token: 0x060038B1 RID: 14513 RVA: 0x000F1F0F File Offset: 0x000F010F
		public DesignerSerializerAttribute(Type serializerType, Type baseSerializerType)
		{
			this.serializerTypeName = serializerType.AssemblyQualifiedName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000F1F2F File Offset: 0x000F012F
		public DesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000F1F4A File Offset: 0x000F014A
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerTypeName;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000F1F60 File Offset: 0x000F0160
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x000F1F68 File Offset: 0x000F0168
		public string SerializerBaseTypeName
		{
			get
			{
				return this.serializerBaseTypeName;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000F1F70 File Offset: 0x000F0170
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.serializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		// Token: 0x04002B79 RID: 11129
		private string serializerTypeName;

		// Token: 0x04002B7A RID: 11130
		private string serializerBaseTypeName;

		// Token: 0x04002B7B RID: 11131
		private string typeId;
	}
}
