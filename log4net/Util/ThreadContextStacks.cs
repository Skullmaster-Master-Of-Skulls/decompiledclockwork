using System;

namespace log4net.Util
{
	// Token: 0x0200011C RID: 284
	public sealed class ThreadContextStacks
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00019BA1 File Offset: 0x00017DA1
		internal ThreadContextStacks(ContextPropertiesBase properties)
		{
			this.m_properties = properties;
		}

		// Token: 0x170001C7 RID: 455
		public ThreadContextStack this[string key]
		{
			get
			{
				object obj = this.m_properties[key];
				ThreadContextStack threadContextStack;
				if (obj == null)
				{
					threadContextStack = new ThreadContextStack();
					this.m_properties[key] = threadContextStack;
				}
				else
				{
					threadContextStack = (obj as ThreadContextStack);
					if (threadContextStack == null)
					{
						string text = SystemInfo.NullText;
						try
						{
							text = obj.ToString();
						}
						catch
						{
						}
						LogLog.Error(ThreadContextStacks.declaringType, string.Concat(new string[]
						{
							"ThreadContextStacks: Request for stack named [",
							key,
							"] failed because a property with the same name exists which is a [",
							obj.GetType().Name,
							"] with value [",
							text,
							"]"
						}));
						threadContextStack = new ThreadContextStack();
					}
				}
				return threadContextStack;
			}
		}

		// Token: 0x04000305 RID: 773
		private readonly ContextPropertiesBase m_properties;

		// Token: 0x04000306 RID: 774
		private static readonly Type declaringType = typeof(ThreadContextStacks);
	}
}
