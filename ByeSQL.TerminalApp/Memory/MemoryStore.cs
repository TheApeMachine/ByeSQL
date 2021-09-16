using ByeSQL.TerminalApp.Handlers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System.Text;

namespace ByeSQL.TerminalApp.Store
{
    class MemoryStore : BaseHandler
    {
        enum OpCode
        {
            VARIABLE, MODIFIER, TYPE, FIELD, INTEGER
        }

        private Ctx ctx;
        private OpCode opCode;

        private Stack<OpCode> opCodes = new Stack<OpCode>();
        private DataSet dataset;
        private DataTableCollection tables;
        private Stack<string> varReg = new Stack<string>();
        private Stack<string> defReg = new Stack<string>();
        private Stack<string> typeReg = new Stack<string>();
        private Stack<string> fieldReg = new Stack<string>();
        private Stack<BigInteger> intReg = new Stack<BigInteger>();

        public MemoryStore()
        {
            this.dataset = new DataSet();
            this.tables = dataset.Tables;
        }

        public void Create(Ctx ctx)
        {
            this.ctx = ctx;
            this.tables.Add(new DataTable());
        }

        public void HandleScope(Stack<string> scope)
        {
            switch (ctx)
            {
                case Ctx.CREATE:
                    while (scope.Count > 0)
                    {
                        this.handleCreateToken(scope.Pop());
                    }
                    break;
            }
        }

        private void handleCreateToken(string token)
        {
            if (variables.Contains(token)) handleVariable(token);
            else if (modifiers.Contains(token)) handleModifier(token);
            else if (types.Contains(token)) handleType(token);
            else handleObject(token);
        }

        private void handleObject(string token)
        {
            BigInteger tmpBigInt;
            bool isNumber = BigInteger.TryParse(token, out tmpBigInt);

            if (isNumber)
            {
                this.intReg.Push(tmpBigInt);
                this.opCodes.Push(OpCode.INTEGER);
            }
            else
            {
                this.fieldReg.Push(token);
                this.opCodes.Push(OpCode.FIELD);
            }
        }

        private void handleVariable(string token)
        {
            if (token == "CURRENT_TIMESTAMP")
            {
                this.varReg.Push(token);
            }

            this.opCodes.Push(OpCode.VARIABLE);
        }

        private void handleModifier(string token)
        {
            if (token == "DEFAULT")
            {
                // We remove the variable from the variable registers
                // and add it to the default registers. This makes it so we
                // have somewhere to look when we encounter NULL values.
                this.defReg.Push(this.varReg.Pop());
            }

            this.opCodes.Push(OpCode.MODIFIER);
        }

        private void handleType(string token)
        {
            if (token == "TIMESTAMP")
            {
                this.typeReg.Push(token);
            }

            this.opCodes.Push(OpCode.TYPE);
        }
    }
}
