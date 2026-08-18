using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog.Common;

namespace NLog.Targets
{
	// Token: 0x02000166 RID: 358
	[Target("MethodCall")]
	public sealed class MethodCallTarget : MethodCallTargetBase
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00020E94 File Offset: 0x0001F094
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00020E9C File Offset: 0x0001F09C
		public string ClassName { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00020EA5 File Offset: 0x0001F0A5
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x00020EAD File Offset: 0x0001F0AD
		public string MethodName { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00020EB6 File Offset: 0x0001F0B6
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x00020EBE File Offset: 0x0001F0BE
		private MethodInfo Method { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00020EC7 File Offset: 0x0001F0C7
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x00020ECF File Offset: 0x0001F0CF
		private int NeededParameters { get; set; }

		// Token: 0x06000DAC RID: 3500 RVA: 0x00020ED8 File Offset: 0x0001F0D8
		public MethodCallTarget()
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		public MethodCallTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (this.ClassName != null && this.MethodName != null)
			{
				Type type = Type.GetType(this.ClassName);
				if (!(type != null))
				{
					InternalLogger.Warn("Initialize MethodCallTarget, class '{0}' not found", new object[]
					{
						this.ClassName
					});
					this.Method = null;
					return;
				}
				this.Method = type.GetMethod(this.MethodName);
				this.NeededParameters = this.Method.GetParameters().Length;
				if (this.Method == null)
				{
					InternalLogger.Warn("Initialize MethodCallTarget, method '{0}' in class '{1}' not found - it should be static", new object[]
					{
						this.Method,
						this.ClassName
					});
					return;
				}
			}
			else
			{
				this.Method = null;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		protected override void DoInvoke(object[] parameters)
		{
			if (this.Method != null)
			{
				int num = this.NeededParameters - parameters.Length;
				if (num > 0)
				{
					List<object> list = new List<object>(parameters);
					list.AddRange(Enumerable.Repeat<object>(Type.Missing, num));
					parameters = list.ToArray();
				}
				this.Method.Invoke(null, parameters);
				return;
			}
			InternalLogger.Trace("No invoke because class/method was not found or set");
		}
	}
}
