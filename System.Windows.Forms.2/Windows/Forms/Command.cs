using System;

namespace System.Windows.Forms
{
	// Token: 0x02000163 RID: 355
	internal class Command : WeakReference
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002BFB9 File Offset: 0x0002A1B9
		public Command(ICommandExecutor target) : base(target, false)
		{
			Command.AssignID(this);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0002BFC9 File Offset: 0x0002A1C9
		public virtual int ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
		protected static void AssignID(Command cmd)
		{
			object obj = Command.internalSyncObject;
			lock (obj)
			{
				int i;
				if (Command.cmds == null)
				{
					Command.cmds = new Command[20];
					i = 0;
				}
				else
				{
					int num = Command.cmds.Length;
					if (Command.icmdTry >= num)
					{
						Command.icmdTry = 0;
					}
					for (i = Command.icmdTry; i < num; i++)
					{
						if (Command.cmds[i] == null)
						{
							goto IL_102;
						}
					}
					for (i = 0; i < Command.icmdTry; i++)
					{
						if (Command.cmds[i] == null)
						{
							goto IL_102;
						}
					}
					for (i = 0; i < num; i++)
					{
						if (Command.cmds[i].Target == null)
						{
							goto IL_102;
						}
					}
					i = Command.cmds.Length;
					num = Math.Min(65280, 2 * i);
					if (num <= i)
					{
						GC.Collect();
						for (i = 0; i < num; i++)
						{
							if (Command.cmds[i] == null || Command.cmds[i].Target == null)
							{
								goto IL_102;
							}
						}
						throw new ArgumentException(SR.GetString("CommandIdNotAllocated"));
					}
					Command[] destinationArray = new Command[num];
					Array.Copy(Command.cmds, 0, destinationArray, 0, i);
					Command.cmds = destinationArray;
				}
				IL_102:
				cmd.id = i + 256;
				Command.cmds[i] = cmd;
				Command.icmdTry = i + 1;
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002C128 File Offset: 0x0002A328
		public static bool DispatchID(int id)
		{
			Command commandFromID = Command.GetCommandFromID(id);
			return commandFromID != null && commandFromID.Invoke();
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002C148 File Offset: 0x0002A348
		protected static void Dispose(Command cmd)
		{
			object obj = Command.internalSyncObject;
			lock (obj)
			{
				if (cmd.id >= 256)
				{
					cmd.Target = null;
					if (Command.cmds[cmd.id - 256] == cmd)
					{
						Command.cmds[cmd.id - 256] = null;
					}
					cmd.id = 0;
				}
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public virtual void Dispose()
		{
			if (this.id >= 256)
			{
				Command.Dispose(this);
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		public static Command GetCommandFromID(int id)
		{
			object obj = Command.internalSyncObject;
			Command result;
			lock (obj)
			{
				if (Command.cmds == null)
				{
					result = null;
				}
				else
				{
					int num = id - 256;
					if (num < 0 || num >= Command.cmds.Length)
					{
						result = null;
					}
					else
					{
						result = Command.cmds[num];
					}
				}
			}
			return result;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002C244 File Offset: 0x0002A444
		public virtual bool Invoke()
		{
			object target = this.Target;
			if (!(target is ICommandExecutor))
			{
				return false;
			}
			((ICommandExecutor)target).Execute();
			return true;
		}

		// Token: 0x040007F4 RID: 2036
		private static Command[] cmds;

		// Token: 0x040007F5 RID: 2037
		private static int icmdTry;

		// Token: 0x040007F6 RID: 2038
		private static object internalSyncObject = new object();

		// Token: 0x040007F7 RID: 2039
		private const int idMin = 256;

		// Token: 0x040007F8 RID: 2040
		private const int idLim = 65536;

		// Token: 0x040007F9 RID: 2041
		internal int id;
	}
}
