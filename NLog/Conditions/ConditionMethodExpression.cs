using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using NLog.Common;

namespace NLog.Conditions
{
	// Token: 0x02000033 RID: 51
	internal sealed class ConditionMethodExpression : ConditionExpression
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003624 File Offset: 0x00001824
		public ConditionMethodExpression(string conditionMethodName, MethodInfo methodInfo, IEnumerable<ConditionExpression> methodParameters)
		{
			this.MethodInfo = methodInfo;
			this.conditionMethodName = conditionMethodName;
			this.MethodParameters = new List<ConditionExpression>(methodParameters).AsReadOnly();
			ParameterInfo[] parameters = this.MethodInfo.GetParameters();
			if (parameters.Length > 0 && parameters[0].ParameterType == typeof(LogEventInfo))
			{
				this.acceptsLogEvent = true;
			}
			int num = this.MethodParameters.Count;
			if (this.acceptsLogEvent)
			{
				num++;
			}
			int num2 = 0;
			int num3 = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsOptional)
				{
					num3++;
				}
				else
				{
					num2++;
				}
			}
			if (num < num2 || num > parameters.Length)
			{
				string message;
				if (num3 > 0)
				{
					message = string.Format(CultureInfo.InvariantCulture, "Condition method '{0}' requires between {1} and {2} parameters, but passed {3}.", new object[]
					{
						conditionMethodName,
						num2,
						parameters.Length,
						num
					});
				}
				else
				{
					message = string.Format(CultureInfo.InvariantCulture, "Condition method '{0}' requires {1} parameters, but passed {2}.", new object[]
					{
						conditionMethodName,
						num2,
						num
					});
				}
				InternalLogger.Error(message);
				throw new ConditionParseException(message);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000376A File Offset: 0x0000196A
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003772 File Offset: 0x00001972
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000377B File Offset: 0x0000197B
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003783 File Offset: 0x00001983
		public IList<ConditionExpression> MethodParameters { get; private set; }

		// Token: 0x060000DB RID: 219 RVA: 0x0000378C File Offset: 0x0000198C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.conditionMethodName);
			stringBuilder.Append("(");
			string value = string.Empty;
			for (int i = 0; i < this.MethodParameters.Count; i++)
			{
				ConditionExpression value2 = this.MethodParameters[i];
				stringBuilder.Append(value);
				stringBuilder.Append(value2);
				value = ", ";
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000380C File Offset: 0x00001A0C
		protected override object EvaluateNode(LogEventInfo context)
		{
			int num = this.acceptsLogEvent ? 1 : 0;
			object[] array = new object[this.MethodParameters.Count + num];
			for (int i = 0; i < this.MethodParameters.Count; i++)
			{
				ConditionExpression conditionExpression = this.MethodParameters[i];
				array[i + num] = conditionExpression.Evaluate(context);
			}
			if (this.acceptsLogEvent)
			{
				array[0] = context;
			}
			return this.MethodInfo.DeclaringType.InvokeMember(this.MethodInfo.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.OptionalParamBinding, null, null, array, CultureInfo.InvariantCulture);
		}

		// Token: 0x04000035 RID: 53
		private readonly bool acceptsLogEvent;

		// Token: 0x04000036 RID: 54
		private readonly string conditionMethodName;
	}
}
