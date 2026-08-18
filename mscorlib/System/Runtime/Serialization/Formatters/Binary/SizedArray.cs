using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007ED RID: 2029
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x060047B0 RID: 18352 RVA: 0x000F5BFF File Offset: 0x000F4BFF
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x000F5C20 File Offset: 0x000F4C20
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x000F5C40 File Offset: 0x000F4C40
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x000F5C9D File Offset: 0x000F4C9D
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x17000C7B RID: 3195
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000F5D34 File Offset: 0x000F4D34
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int num = Math.Max(this.negObjects.Length * 2, -index + 1);
					object[] destinationArray = new object[num];
					Array.Copy(this.negObjects, 0, destinationArray, 0, this.negObjects.Length);
					this.negObjects = destinationArray;
				}
				else
				{
					int num2 = Math.Max(this.objects.Length * 2, index + 1);
					object[] destinationArray2 = new object[num2];
					Array.Copy(this.objects, 0, destinationArray2, 0, this.objects.Length);
					this.objects = destinationArray2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
			}
		}

		// Token: 0x04002489 RID: 9353
		internal object[] objects;

		// Token: 0x0400248A RID: 9354
		internal object[] negObjects;
	}
}
