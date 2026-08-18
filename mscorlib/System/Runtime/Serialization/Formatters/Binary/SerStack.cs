using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007EC RID: 2028
	internal sealed class SerStack
	{
		// Token: 0x060047A6 RID: 18342 RVA: 0x000F5A82 File Offset: 0x000F4A82
		internal SerStack()
		{
			this.stackId = "System";
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x000F5AA8 File Offset: 0x000F4AA8
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x000F5ACC File Offset: 0x000F4ACC
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			this.objects[++this.top] = obj;
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x000F5B0C File Offset: 0x000F4B0C
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object result = this.objects[this.top];
			this.objects[this.top--] = null;
			return result;
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x000F5B4C File Offset: 0x000F4B4C
		internal void IncreaseCapacity()
		{
			int num = this.objects.Length * 2;
			object[] destinationArray = new object[num];
			Array.Copy(this.objects, 0, destinationArray, 0, this.objects.Length);
			this.objects = destinationArray;
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x000F5B88 File Offset: 0x000F4B88
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x000F5BA2 File Offset: 0x000F4BA2
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x000F5BBE File Offset: 0x000F4BBE
		internal int Count()
		{
			return this.top + 1;
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x000F5BC8 File Offset: 0x000F4BC8
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000F5BD8 File Offset: 0x000F4BD8
		[Conditional("SER_LOGGING")]
		internal void Dump()
		{
			for (int i = 0; i < this.Count(); i++)
			{
				object obj = this.objects[i];
			}
		}

		// Token: 0x04002485 RID: 9349
		internal object[] objects = new object[5];

		// Token: 0x04002486 RID: 9350
		internal string stackId;

		// Token: 0x04002487 RID: 9351
		internal int top = -1;

		// Token: 0x04002488 RID: 9352
		internal int next;
	}
}
