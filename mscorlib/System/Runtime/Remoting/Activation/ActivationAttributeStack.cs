using System;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020006A4 RID: 1700
	internal class ActivationAttributeStack
	{
		// Token: 0x06003D6E RID: 15726 RVA: 0x000D22AF File Offset: 0x000D12AF
		internal ActivationAttributeStack()
		{
			this.activationTypes = new object[4];
			this.activationAttributes = new object[4];
			this.freeIndex = 0;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000D22D8 File Offset: 0x000D12D8
		internal void Push(Type typ, object[] attr)
		{
			if (this.freeIndex == this.activationTypes.Length)
			{
				object[] destinationArray = new object[this.activationTypes.Length * 2];
				object[] destinationArray2 = new object[this.activationAttributes.Length * 2];
				Array.Copy(this.activationTypes, destinationArray, this.activationTypes.Length);
				Array.Copy(this.activationAttributes, destinationArray2, this.activationAttributes.Length);
				this.activationTypes = destinationArray;
				this.activationAttributes = destinationArray2;
			}
			this.activationTypes[this.freeIndex] = typ;
			this.activationAttributes[this.freeIndex] = attr;
			this.freeIndex++;
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x000D2375 File Offset: 0x000D1375
		internal object[] Peek(Type typ)
		{
			if (this.freeIndex == 0 || this.activationTypes[this.freeIndex - 1] != typ)
			{
				return null;
			}
			return (object[])this.activationAttributes[this.freeIndex - 1];
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000D23A8 File Offset: 0x000D13A8
		internal void Pop(Type typ)
		{
			if (this.freeIndex != 0 && this.activationTypes[this.freeIndex - 1] == typ)
			{
				this.freeIndex--;
				this.activationTypes[this.freeIndex] = null;
				this.activationAttributes[this.freeIndex] = null;
			}
		}

		// Token: 0x04001F6D RID: 8045
		private object[] activationTypes;

		// Token: 0x04001F6E RID: 8046
		private object[] activationAttributes;

		// Token: 0x04001F6F RID: 8047
		private int freeIndex;
	}
}
