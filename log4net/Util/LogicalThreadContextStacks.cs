using System;

namespace log4net.Util
{
	// Token: 0x02000102 RID: 258
	public sealed class LogicalThreadContextStacks
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x000175BB File Offset: 0x000157BB
		internal LogicalThreadContextStacks(LogicalThreadContextProperties properties)
		{
			this.m_properties = properties;
		}

		// Token: 0x17000183 RID: 387
		public LogicalThreadContextStack this[string key]
		{
			get
			{
				object obj = this.m_properties[key];
				LogicalThreadContextStack logicalThreadContextStack;
				if (obj == null)
				{
					logicalThreadContextStack = new LogicalThreadContextStack(key, new TwoArgAction<string, LogicalThreadContextStack>(this.registerNew));
					this.m_properties[key] = logicalThreadContextStack;
				}
				else
				{
					logicalThreadContextStack = (obj as LogicalThreadContextStack);
					if (logicalThreadContextStack == null)
					{
						string text = SystemInfo.NullText;
						try
						{
							text = obj.ToString();
						}
						catch
						{
						}
						LogLog.Error(LogicalThreadContextStacks.declaringType, string.Concat(new string[]
						{
							"ThreadContextStacks: Request for stack named [",
							key,
							"] failed because a property with the same name exists which is a [",
							obj.GetType().Name,
							"] with value [",
							text,
							"]"
						}));
						logicalThreadContextStack = new LogicalThreadContextStack(key, new TwoArgAction<string, LogicalThreadContextStack>(this.registerNew));
					}
				}
				return logicalThreadContextStack;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001769C File Offset: 0x0001589C
		private void registerNew(string stackName, LogicalThreadContextStack stack)
		{
			this.m_properties[stackName] = stack;
		}

		// Token: 0x040002C0 RID: 704
		private readonly LogicalThreadContextProperties m_properties;

		// Token: 0x040002C1 RID: 705
		private static readonly Type declaringType = typeof(LogicalThreadContextStacks);
	}
}
