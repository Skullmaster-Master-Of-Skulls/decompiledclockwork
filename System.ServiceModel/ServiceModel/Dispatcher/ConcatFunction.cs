using System;
using System.Text;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A0 RID: 1184
	internal class ConcatFunction : QueryFunction
	{
		// Token: 0x06002D45 RID: 11589 RVA: 0x000B09B2 File Offset: 0x000AEBB2
		internal ConcatFunction(int argCount) : base("concat", ValueDataType.String, ConcatFunction.MakeTypes(argCount))
		{
			this.argCount = argCount;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000B09D0 File Offset: 0x000AEBD0
		internal override bool Equals(QueryFunction function)
		{
			ConcatFunction concatFunction = function as ConcatFunction;
			return concatFunction != null && this.argCount == concatFunction.argCount;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000B09F8 File Offset: 0x000AEBF8
		internal override void Eval(ProcessingContext context)
		{
			StackFrame[] array = new StackFrame[this.argCount];
			for (int i = 0; i < this.argCount; i++)
			{
				array[i] = context[i];
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (array[0].basePtr <= array[0].endPtr)
			{
				stringBuilder.Length = 0;
				for (int j = 0; j < this.argCount; j++)
				{
					stringBuilder.Append(context.PeekString(array[j].basePtr));
				}
				context.SetValue(context, array[this.argCount - 1].basePtr, stringBuilder.ToString());
				for (int k = 0; k < this.argCount; k++)
				{
					StackFrame[] array2 = array;
					int num = k;
					array2[num].basePtr = array2[num].basePtr + 1;
				}
			}
			for (int l = 0; l < this.argCount - 1; l++)
			{
				context.PopFrame();
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000B0AEC File Offset: 0x000AECEC
		internal static ValueDataType[] MakeTypes(int size)
		{
			ValueDataType[] array = new ValueDataType[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = ValueDataType.String;
			}
			return array;
		}

		// Token: 0x040024CA RID: 9418
		private int argCount;
	}
}
