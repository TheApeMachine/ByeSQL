using ByeSQL.TerminalApp.Store;
using System.Collections.Generic;

namespace ByeSQL.TerminalApp.Handlers
{
    class ScopeHandler : BaseHandler
    {
        private MemoryStore store = new MemoryStore();

        public void Execute(Ctx ctx, bool debug, Stack<string> scope)
        {
            switch (ctx)
            {
                case Ctx.CREATE:
                    this.store.Create(ctx);
                    break;
            }

            this.store.HandleScope(scope);
        }
    }
}
