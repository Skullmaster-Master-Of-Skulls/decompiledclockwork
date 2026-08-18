using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001B4 RID: 436
	internal sealed class SqlCommandSet
	{
		// Token: 0x06001A1F RID: 6687 RVA: 0x000B9F1C File Offset: 0x000B931C
		internal SqlCommandSet()
		{
			this._batchCommand = new SqlCommand();
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000B9F58 File Offset: 0x000B9358
		private SqlCommand BatchCommand
		{
			get
			{
				SqlCommand batchCommand = this._batchCommand;
				if (batchCommand == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return batchCommand;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000B9F78 File Offset: 0x000B9378
		internal int CommandCount
		{
			get
			{
				return this.CommandList.Count;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x000B9F90 File Offset: 0x000B9390
		private List<SqlCommandSet.LocalCommand> CommandList
		{
			get
			{
				List<SqlCommandSet.LocalCommand> commandList = this._commandList;
				if (commandList == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return commandList;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x000B9FB0 File Offset: 0x000B93B0
		internal int CommandTimeout
		{
			set
			{
				this.BatchCommand.CommandTimeout = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x000B9FCC File Offset: 0x000B93CC
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x000B9FE4 File Offset: 0x000B93E4
		internal SqlConnection Connection
		{
			get
			{
				return this.BatchCommand.Connection;
			}
			set
			{
				this.BatchCommand.Connection = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x000BA000 File Offset: 0x000B9400
		internal SqlTransaction Transaction
		{
			set
			{
				this.BatchCommand.Transaction = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x000BA01C File Offset: 0x000B941C
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000BA030 File Offset: 0x000B9430
		internal void Append(SqlCommand command)
		{
			ADP.CheckArgumentNull(command, "command");
			Bid.Trace("<sc.SqlCommandSet.Append|API> %d#, command=%d, parameterCount=%d\n", this.ObjectID, command.ObjectID, command.Parameters.Count);
			string commandText = command.CommandText;
			if (ADP.IsEmpty(commandText))
			{
				throw ADP.CommandTextRequired("Append");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text || commandType == CommandType.StoredProcedure)
			{
				SqlParameterCollection sqlParameterCollection = null;
				SqlParameterCollection parameters = command.Parameters;
				if (0 < parameters.Count)
				{
					sqlParameterCollection = new SqlParameterCollection();
					for (int i = 0; i < parameters.Count; i++)
					{
						SqlParameter sqlParameter = new SqlParameter();
						parameters[i].CopyTo(sqlParameter);
						sqlParameterCollection.Add(sqlParameter);
						if (!SqlCommandSet.SqlIdentifierParser.IsMatch(sqlParameter.ParameterName))
						{
							throw ADP.BadParameterName(sqlParameter.ParameterName);
						}
					}
					foreach (object obj in sqlParameterCollection)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj;
						object value = sqlParameter2.Value;
						byte[] array = value as byte[];
						if (array != null)
						{
							int offset = sqlParameter2.Offset;
							int size = sqlParameter2.Size;
							int num = array.Length - offset;
							if (size != 0 && size < num)
							{
								num = size;
							}
							byte[] array2 = new byte[Math.Max(num, 0)];
							Buffer.BlockCopy(array, offset, array2, 0, array2.Length);
							sqlParameter2.Offset = 0;
							sqlParameter2.Value = array2;
						}
						else
						{
							char[] array3 = value as char[];
							if (array3 != null)
							{
								int offset2 = sqlParameter2.Offset;
								int size2 = sqlParameter2.Size;
								int num2 = array3.Length - offset2;
								if (size2 != 0 && size2 < num2)
								{
									num2 = size2;
								}
								char[] array4 = new char[Math.Max(num2, 0)];
								Buffer.BlockCopy(array3, offset2, array4, 0, array4.Length * 2);
								sqlParameter2.Offset = 0;
								sqlParameter2.Value = array4;
							}
							else
							{
								ICloneable cloneable = value as ICloneable;
								if (cloneable != null)
								{
									sqlParameter2.Value = cloneable.Clone();
								}
							}
						}
					}
				}
				int returnParameterIndex = -1;
				if (sqlParameterCollection != null)
				{
					for (int j = 0; j < sqlParameterCollection.Count; j++)
					{
						if (ParameterDirection.ReturnValue == sqlParameterCollection[j].Direction)
						{
							returnParameterIndex = j;
							break;
						}
					}
				}
				SqlCommandSet.LocalCommand item = new SqlCommandSet.LocalCommand(commandText, sqlParameterCollection, returnParameterIndex, command.CommandType, command.ColumnEncryptionSetting);
				this.CommandList.Add(item);
				return;
			}
			if (commandType == CommandType.TableDirect)
			{
				throw SQL.NotSupportedCommandType(commandType);
			}
			throw ADP.InvalidCommandType(commandType);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000BA2B8 File Offset: 0x000B96B8
		internal static void BuildStoredProcedureName(StringBuilder builder, string part)
		{
			if (part != null && 0 < part.Length)
			{
				if ('[' == part[0])
				{
					int num = 0;
					foreach (char c in part)
					{
						if (']' == c)
						{
							num++;
						}
					}
					if (1 == num % 2)
					{
						builder.Append(part);
						return;
					}
				}
				SqlServerEscapeHelper.EscapeIdentifier(builder, part);
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000BA318 File Offset: 0x000B9718
		internal void Clear()
		{
			Bid.Trace("<sc.SqlCommandSet.Clear|API> %d#\n", this.ObjectID);
			DbCommand batchCommand = this.BatchCommand;
			if (batchCommand != null)
			{
				batchCommand.Parameters.Clear();
				batchCommand.CommandText = null;
			}
			List<SqlCommandSet.LocalCommand> commandList = this._commandList;
			if (commandList != null)
			{
				commandList.Clear();
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000BA364 File Offset: 0x000B9764
		internal void Dispose()
		{
			Bid.Trace("<sc.SqlCommandSet.Dispose|API> %d#\n", this.ObjectID);
			SqlCommand batchCommand = this._batchCommand;
			this._commandList = null;
			this._batchCommand = null;
			if (batchCommand != null)
			{
				batchCommand.Dispose();
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000BA3A0 File Offset: 0x000B97A0
		internal int ExecuteNonQuery()
		{
			SqlConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommandSet.ExecuteNonQuery|API> %d#", this.ObjectID);
			int result;
			try
			{
				if (this.Connection.IsContextConnection)
				{
					throw SQL.BatchedUpdatesNotAvailableOnContextConnection();
				}
				this.ValidateCommandBehavior("ExecuteNonQuery", CommandBehavior.Default);
				this.BatchCommand.BatchRPCMode = true;
				this.BatchCommand.ClearBatchCommand();
				this.BatchCommand.Parameters.Clear();
				for (int i = 0; i < this._commandList.Count; i++)
				{
					SqlCommandSet.LocalCommand localCommand = this._commandList[i];
					this.BatchCommand.AddBatchCommand(localCommand.CommandText, localCommand.Parameters, localCommand.CmdType, localCommand.ColumnEncryptionSetting);
				}
				result = this.BatchCommand.ExecuteBatchRPCCommand();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000BA488 File Offset: 0x000B9888
		internal SqlParameter GetParameter(int commandIndex, int parameterIndex)
		{
			return this.CommandList[commandIndex].Parameters[parameterIndex];
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000BA4AC File Offset: 0x000B98AC
		internal bool GetBatchedAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			error = this.BatchCommand.GetErrors(commandIdentifier);
			int? recordsAffected2 = this.BatchCommand.GetRecordsAffected(commandIdentifier);
			recordsAffected = recordsAffected2.GetValueOrDefault();
			return recordsAffected2 != null;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000BA4E4 File Offset: 0x000B98E4
		internal int GetParameterCount(int commandIndex)
		{
			return this.CommandList[commandIndex].Parameters.Count;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000BA508 File Offset: 0x000B9908
		private void ValidateCommandBehavior(string method, CommandBehavior behavior)
		{
			if ((behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection)) != CommandBehavior.Default)
			{
				ADP.ValidateCommandBehavior(behavior);
				throw ADP.NotSupportedCommandBehavior(behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection), method);
			}
		}

		// Token: 0x04000F3D RID: 3901
		private const string SqlIdentifierPattern = "^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}＿_@#\\$]*$";

		// Token: 0x04000F3E RID: 3902
		private static readonly Regex SqlIdentifierParser = new Regex("^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}＿_@#\\$]*$", RegexOptions.ExplicitCapture | RegexOptions.Singleline);

		// Token: 0x04000F3F RID: 3903
		private List<SqlCommandSet.LocalCommand> _commandList = new List<SqlCommandSet.LocalCommand>();

		// Token: 0x04000F40 RID: 3904
		private SqlCommand _batchCommand;

		// Token: 0x04000F41 RID: 3905
		private static int _objectTypeCount;

		// Token: 0x04000F42 RID: 3906
		internal readonly int _objectID = Interlocked.Increment(ref SqlCommandSet._objectTypeCount);

		// Token: 0x020003A2 RID: 930
		private sealed class LocalCommand
		{
			// Token: 0x060034D2 RID: 13522 RVA: 0x001427D8 File Offset: 0x00141BD8
			internal LocalCommand(string commandText, SqlParameterCollection parameters, int returnParameterIndex, CommandType cmdType, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
			{
				this.CommandText = commandText;
				this.Parameters = parameters;
				this.ReturnParameterIndex = returnParameterIndex;
				this.CmdType = cmdType;
				this.ColumnEncryptionSetting = columnEncryptionSetting;
			}

			// Token: 0x0400200C RID: 8204
			internal readonly string CommandText;

			// Token: 0x0400200D RID: 8205
			internal readonly SqlParameterCollection Parameters;

			// Token: 0x0400200E RID: 8206
			internal readonly int ReturnParameterIndex;

			// Token: 0x0400200F RID: 8207
			internal readonly CommandType CmdType;

			// Token: 0x04002010 RID: 8208
			internal readonly SqlCommandColumnEncryptionSetting ColumnEncryptionSetting;
		}
	}
}
