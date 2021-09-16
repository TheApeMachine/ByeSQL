using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using ByeSQL.TerminalApp.Handlers;
using System;
using System.Reflection;
using Workbench.Parsers;

namespace ByeSQL.TerminalApp
{
    class TestVisitor : IMySQLParserVisitor<IParseTree>
    {
        // Adds handlers for command groups.
        private IContextHandler handler = new QueryHandler();
        private bool debug = false;

        private void NextVisit(IParseTree obj, int index)
        {
            // Make sure we're looking at the next object, since we're still in the same
            // context as we started.
            IParseTree curobj = obj.GetChild(index);

            // Convert the MySQLParserContext type to a string that mathes the recursive methods below.
            string name = curobj.GetType().ToString().Replace("Workbench.Parsers.MySQLParser+", "").Replace("Context", "");            
            string meth = "Visit" + name;

            // Get the method info of the recursive method by string reference.
            MethodInfo method = this.GetType().GetMethod(meth);

            try
            {
                if (this.debug) Console.WriteLine(curobj.GetType());

                // Try to invoke the method to continue to the next recursive step.
                method.Invoke(this, new IParseTree[] { curobj });
            }
            catch (NullReferenceException)
            {
                if (this.debug) Console.WriteLine(curobj.GetText());

                // Alter the states of the internal stacks of the handler.
                handler.Handle(curobj);
            }
        }

        private IParseTree Fold(IParseTree context)
        {
            for (int i = 0; i < context.ChildCount; i++)
            {
                // NextVisit will look at the next element in the current context and change
                // state of the internal stacks of handlers.
                NextVisit(context, i);
            }

            // Return the context so we respect the callers return type and we're recursive.
            return context;
        }

        public IParseTree Visit(IParseTree tree)
        {
            // This prints verbose information about the MySQLParser types we are dealing with.
            this.debug = false;

            // Trigger the recursion. We are not concerned with all the different methods below.
            // Send it to the recursive method above and start attaching handlers to events.
            Fold(tree);
            this.handler.Execute(true);

            return tree;
        }

        // Mostly ignore this. I had to deal with the interface ANTLR gave me.
        public IParseTree VisitAccountLockPasswordExpireOptions([NotNull] MySQLParser.AccountLockPasswordExpireOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitAccountManagementStatement([NotNull] MySQLParser.AccountManagementStatementContext context)
        { return Fold(context); }

        public IParseTree VisitAclType([NotNull] MySQLParser.AclTypeContext context)
        { return Fold(context); }

        public IParseTree VisitAdminPartition([NotNull] MySQLParser.AdminPartitionContext context)
        { return Fold(context); }

        public IParseTree VisitAllOrPartitionNameList([NotNull] MySQLParser.AllOrPartitionNameListContext context)
        { return Fold(context); }

        public IParseTree VisitAlterAlgorithmOption([NotNull] MySQLParser.AlterAlgorithmOptionContext context)
        { return Fold(context); }

        public IParseTree VisitAlterCommandList([NotNull] MySQLParser.AlterCommandListContext context)
        { return Fold(context); }

        public IParseTree VisitAlterCommandsModifier([NotNull] MySQLParser.AlterCommandsModifierContext context)
        { return Fold(context); }

        public IParseTree VisitAlterCommandsModifierList([NotNull] MySQLParser.AlterCommandsModifierListContext context)
        { return Fold(context); }

        public IParseTree VisitAlterDatabase([NotNull] MySQLParser.AlterDatabaseContext context)
        { return Fold(context); }

        public IParseTree VisitAlterEvent([NotNull] MySQLParser.AlterEventContext context)
        { return Fold(context); }

        public IParseTree VisitAlterList([NotNull] MySQLParser.AlterListContext context)
        { return Fold(context); }

        public IParseTree VisitAlterListItem([NotNull] MySQLParser.AlterListItemContext context)
        { return Fold(context); }

        public IParseTree VisitAlterLockOption([NotNull] MySQLParser.AlterLockOptionContext context)
        { return Fold(context); }

        public IParseTree VisitAlterLogfileGroup([NotNull] MySQLParser.AlterLogfileGroupContext context)
        { return Fold(context); }

        public IParseTree VisitAlterLogfileGroupOption([NotNull] MySQLParser.AlterLogfileGroupOptionContext context)
        { return Fold(context); }

        public IParseTree VisitAlterLogfileGroupOptions([NotNull] MySQLParser.AlterLogfileGroupOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitAlterOrderList([NotNull] MySQLParser.AlterOrderListContext context)
        { return Fold(context); }

        public IParseTree VisitAlterPartition([NotNull] MySQLParser.AlterPartitionContext context)
        { return Fold(context); }

        public IParseTree VisitAlterResourceGroup([NotNull] MySQLParser.AlterResourceGroupContext context)
        { return Fold(context); }

        public IParseTree VisitAlterServer([NotNull] MySQLParser.AlterServerContext context)
        { return Fold(context); }

        public IParseTree VisitAlterStatement([NotNull] MySQLParser.AlterStatementContext context)
        { return Fold(context); }

        public IParseTree VisitAlterTable([NotNull] MySQLParser.AlterTableContext context)
        { return Fold(context); }

        public IParseTree VisitAlterTableActions([NotNull] MySQLParser.AlterTableActionsContext context)
        { return Fold(context); }

        public IParseTree VisitAlterTablespace([NotNull] MySQLParser.AlterTablespaceContext context)
        { return Fold(context); }

        public IParseTree VisitAlterTablespaceOption([NotNull] MySQLParser.AlterTablespaceOptionContext context)
        { return Fold(context); }

        public IParseTree VisitAlterTablespaceOptions([NotNull] MySQLParser.AlterTablespaceOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitAlterUndoTablespace([NotNull] MySQLParser.AlterUndoTablespaceContext context)
        { return Fold(context); }

        public IParseTree VisitAlterUser([NotNull] MySQLParser.AlterUserContext context)
        { return Fold(context); }

        public IParseTree VisitAlterUserEntry([NotNull] MySQLParser.AlterUserEntryContext context)
        { return Fold(context); }

        public IParseTree VisitAlterUserList([NotNull] MySQLParser.AlterUserListContext context)
        { return Fold(context); }

        public IParseTree VisitAlterUserTail([NotNull] MySQLParser.AlterUserTailContext context)
        { return Fold(context); }

        public IParseTree VisitAlterView([NotNull] MySQLParser.AlterViewContext context)
        { return Fold(context); }

        public IParseTree VisitArrayCast([NotNull] MySQLParser.ArrayCastContext context)
        { return Fold(context); }

        public IParseTree VisitAscii([NotNull] MySQLParser.AsciiContext context)
        { return Fold(context); }

        public IParseTree VisitAssignToKeycache([NotNull] MySQLParser.AssignToKeycacheContext context)
        { return Fold(context); }

        public IParseTree VisitAssignToKeycachePartition([NotNull] MySQLParser.AssignToKeycachePartitionContext context)
        { return Fold(context); }

        public IParseTree VisitBeginEndBlock([NotNull] MySQLParser.BeginEndBlockContext context)
        { return Fold(context); }

        public IParseTree VisitBeginWork([NotNull] MySQLParser.BeginWorkContext context)
        { return Fold(context); }

        public IParseTree VisitBitExpr([NotNull] MySQLParser.BitExprContext context)
        { return Fold(context); }

        public IParseTree VisitBoolLiteral([NotNull] MySQLParser.BoolLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitCacheKeyList([NotNull] MySQLParser.CacheKeyListContext context)
        { return Fold(context); }

        public IParseTree VisitCallStatement([NotNull] MySQLParser.CallStatementContext context)
        { return Fold(context); }

        public IParseTree VisitCaseStatement([NotNull] MySQLParser.CaseStatementContext context)
        { return Fold(context); }

        public IParseTree VisitCastType([NotNull] MySQLParser.CastTypeContext context)
        { return Fold(context); }

        public IParseTree VisitChangeMaster([NotNull] MySQLParser.ChangeMasterContext context)
        { return Fold(context); }

        public IParseTree VisitChangeMasterOptions([NotNull] MySQLParser.ChangeMasterOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitChangeReplication([NotNull] MySQLParser.ChangeReplicationContext context)
        { return Fold(context); }

        public IParseTree VisitChangeTablespaceOption([NotNull] MySQLParser.ChangeTablespaceOptionContext context)
        { return Fold(context); }

        public IParseTree VisitChannel([NotNull] MySQLParser.ChannelContext context)
        { return Fold(context); }

        public IParseTree VisitCharset([NotNull] MySQLParser.CharsetContext context)
        { return Fold(context); }

        public IParseTree VisitCharsetClause([NotNull] MySQLParser.CharsetClauseContext context)
        { return Fold(context); }

        public IParseTree VisitCharsetName([NotNull] MySQLParser.CharsetNameContext context)
        { return Fold(context); }

        public IParseTree VisitCharsetWithOptBinary([NotNull] MySQLParser.CharsetWithOptBinaryContext context)
        { return Fold(context); }

        public IParseTree VisitCheckConstraint([NotNull] MySQLParser.CheckConstraintContext context)
        { return Fold(context); }

        public IParseTree VisitCheckOption([NotNull] MySQLParser.CheckOptionContext context)
        { return Fold(context); }

        public IParseTree VisitCheckOrReferences([NotNull] MySQLParser.CheckOrReferencesContext context)
        { return Fold(context); }

        public IParseTree VisitChildren(IRuleNode node)
        { return Fold(node); }

        public IParseTree VisitCloneStatement([NotNull] MySQLParser.CloneStatementContext context)
        { return Fold(context); }

        public IParseTree VisitCollate([NotNull] MySQLParser.CollateContext context)
        { return Fold(context); }

        public IParseTree VisitCollationName([NotNull] MySQLParser.CollationNameContext context)
        { return Fold(context); }

        public IParseTree VisitColumnAttribute([NotNull] MySQLParser.ColumnAttributeContext context)
        { return Fold(context); }

        public IParseTree VisitColumnDefinition([NotNull] MySQLParser.ColumnDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitColumnFormat([NotNull] MySQLParser.ColumnFormatContext context)
        { return Fold(context); }

        public IParseTree VisitColumnInternalRef([NotNull] MySQLParser.ColumnInternalRefContext context)
        { return Fold(context); }

        public IParseTree VisitColumnInternalRefList([NotNull] MySQLParser.ColumnInternalRefListContext context)
        { return Fold(context); }

        public IParseTree VisitColumnName([NotNull] MySQLParser.ColumnNameContext context)
        { return Fold(context); }

        public IParseTree VisitColumnRef([NotNull] MySQLParser.ColumnRefContext context)
        { return Fold(context); }

        public IParseTree VisitColumnsClause([NotNull] MySQLParser.ColumnsClauseContext context)
        { return Fold(context); }

        public IParseTree VisitCommonIndexOption([NotNull] MySQLParser.CommonIndexOptionContext context)
        { return Fold(context); }

        public IParseTree VisitCommonTableExpression([NotNull] MySQLParser.CommonTableExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitComponentRef([NotNull] MySQLParser.ComponentRefContext context)
        { return Fold(context); }

        public IParseTree VisitCompOp([NotNull] MySQLParser.CompOpContext context)
        { return Fold(context); }

        public IParseTree VisitCompoundStatement([NotNull] MySQLParser.CompoundStatementContext context)
        { return Fold(context); }

        public IParseTree VisitCompoundStatementList([NotNull] MySQLParser.CompoundStatementListContext context)
        { return Fold(context); }

        public IParseTree VisitConditionDeclaration([NotNull] MySQLParser.ConditionDeclarationContext context)
        { return Fold(context); }

        public IParseTree VisitConditionInformationItem([NotNull] MySQLParser.ConditionInformationItemContext context)
        { return Fold(context); }

        public IParseTree VisitConnectOptions([NotNull] MySQLParser.ConnectOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitConstraintEnforcement([NotNull] MySQLParser.ConstraintEnforcementContext context)
        { return Fold(context); }

        public IParseTree VisitConstraintKeyType([NotNull] MySQLParser.ConstraintKeyTypeContext context)
        { return Fold(context); }

        public IParseTree VisitConstraintName([NotNull] MySQLParser.ConstraintNameContext context)
        { return Fold(context); }

        public IParseTree VisitCreateDatabase([NotNull] MySQLParser.CreateDatabaseContext context)
        { return Fold(context); }

        public IParseTree VisitCreateDatabaseOption([NotNull] MySQLParser.CreateDatabaseOptionContext context)
        { return Fold(context); }

        public IParseTree VisitCreateEvent([NotNull] MySQLParser.CreateEventContext context)
        { return Fold(context); }

        public IParseTree VisitCreateFunction([NotNull] MySQLParser.CreateFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitCreateIndex([NotNull] MySQLParser.CreateIndexContext context)
        { return Fold(context); }

        public IParseTree VisitCreateIndexTarget([NotNull] MySQLParser.CreateIndexTargetContext context)
        { return Fold(context); }

        public IParseTree VisitCreateLogfileGroup([NotNull] MySQLParser.CreateLogfileGroupContext context)
        { return Fold(context); }

        public IParseTree VisitCreateProcedure([NotNull] MySQLParser.CreateProcedureContext context)
        { return Fold(context); }

        public IParseTree VisitCreateResourceGroup([NotNull] MySQLParser.CreateResourceGroupContext context)
        { return Fold(context); }

        public IParseTree VisitCreateRole([NotNull] MySQLParser.CreateRoleContext context)
        { return Fold(context); }

        public IParseTree VisitCreateRoutine([NotNull] MySQLParser.CreateRoutineContext context)
        { return Fold(context); }

        public IParseTree VisitCreateServer([NotNull] MySQLParser.CreateServerContext context)
        { return Fold(context); }

        public IParseTree VisitCreateSpatialReference([NotNull] MySQLParser.CreateSpatialReferenceContext context)
        { return Fold(context); }

        public IParseTree VisitCreateStatement([NotNull] MySQLParser.CreateStatementContext context)
        { return Fold(context); }

        public IParseTree VisitCreateTable([NotNull] MySQLParser.CreateTableContext context)
        { return Fold(context); }

        public IParseTree VisitCreateTableOption([NotNull] MySQLParser.CreateTableOptionContext context)
        { return Fold(context); }

        public IParseTree VisitCreateTableOptions([NotNull] MySQLParser.CreateTableOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitCreateTableOptionsSpaceSeparated([NotNull] MySQLParser.CreateTableOptionsSpaceSeparatedContext context)
        { return Fold(context); }

        public IParseTree VisitCreateTablespace([NotNull] MySQLParser.CreateTablespaceContext context)
        { return Fold(context); }

        public IParseTree VisitCreateTrigger([NotNull] MySQLParser.CreateTriggerContext context)
        { return Fold(context); }

        public IParseTree VisitCreateUdf([NotNull] MySQLParser.CreateUdfContext context)
        { return Fold(context); }

        public IParseTree VisitCreateUndoTablespace([NotNull] MySQLParser.CreateUndoTablespaceContext context)
        { return Fold(context); }

        public IParseTree VisitCreateUser([NotNull] MySQLParser.CreateUserContext context)
        { return Fold(context); }

        public IParseTree VisitCreateUserEntry([NotNull] MySQLParser.CreateUserEntryContext context)
        { return Fold(context); }

        public IParseTree VisitCreateUserList([NotNull] MySQLParser.CreateUserListContext context)
        { return Fold(context); }

        public IParseTree VisitCreateUserTail([NotNull] MySQLParser.CreateUserTailContext context)
        { return Fold(context); }

        public IParseTree VisitCreateView([NotNull] MySQLParser.CreateViewContext context)
        { return Fold(context); }

        public IParseTree VisitCursorClose([NotNull] MySQLParser.CursorCloseContext context)
        { return Fold(context); }

        public IParseTree VisitCursorDeclaration([NotNull] MySQLParser.CursorDeclarationContext context)
        { return Fold(context); }

        public IParseTree VisitCursorFetch([NotNull] MySQLParser.CursorFetchContext context)
        { return Fold(context); }

        public IParseTree VisitCursorOpen([NotNull] MySQLParser.CursorOpenContext context)
        { return Fold(context); }

        public IParseTree VisitDataDirSSL([NotNull] MySQLParser.DataDirSSLContext context)
        { return Fold(context); }

        public IParseTree VisitDataOrXml([NotNull] MySQLParser.DataOrXmlContext context)
        { return Fold(context); }

        public IParseTree VisitDataType([NotNull] MySQLParser.DataTypeContext context)
        { return Fold(context); }

        public IParseTree VisitDataTypeDefinition([NotNull] MySQLParser.DataTypeDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitDateTimeTtype([NotNull] MySQLParser.DateTimeTtypeContext context)
        { return Fold(context); }

        public IParseTree VisitDefaultCharset([NotNull] MySQLParser.DefaultCharsetContext context)
        { return Fold(context); }

        public IParseTree VisitDefaultCollation([NotNull] MySQLParser.DefaultCollationContext context)
        { return Fold(context); }

        public IParseTree VisitDefaultEncryption([NotNull] MySQLParser.DefaultEncryptionContext context)
        { return Fold(context); }

        public IParseTree VisitDefaultRoleClause([NotNull] MySQLParser.DefaultRoleClauseContext context)
        { return Fold(context); }

        public IParseTree VisitDefinerClause([NotNull] MySQLParser.DefinerClauseContext context)
        { return Fold(context); }

        public IParseTree VisitDeleteOption([NotNull] MySQLParser.DeleteOptionContext context)
        { return Fold(context); }

        public IParseTree VisitDeleteStatement([NotNull] MySQLParser.DeleteStatementContext context)
        { return Fold(context); }

        public IParseTree VisitDeleteStatementOption([NotNull] MySQLParser.DeleteStatementOptionContext context)
        { return Fold(context); }

        public IParseTree VisitDerivedTable([NotNull] MySQLParser.DerivedTableContext context)
        { return Fold(context); }

        public IParseTree VisitDescribeCommand([NotNull] MySQLParser.DescribeCommandContext context)
        { return Fold(context); }

        public IParseTree VisitDirection([NotNull] MySQLParser.DirectionContext context)
        { return Fold(context); }

        public IParseTree VisitDiscardOldPassword([NotNull] MySQLParser.DiscardOldPasswordContext context)
        { return Fold(context); }

        public IParseTree VisitDoStatement([NotNull] MySQLParser.DoStatementContext context)
        { return Fold(context); }

        public IParseTree VisitDotIdentifier([NotNull] MySQLParser.DotIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitDropDatabase([NotNull] MySQLParser.DropDatabaseContext context)
        { return Fold(context); }

        public IParseTree VisitDropEvent([NotNull] MySQLParser.DropEventContext context)
        { return Fold(context); }

        public IParseTree VisitDropFunction([NotNull] MySQLParser.DropFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitDropIndex([NotNull] MySQLParser.DropIndexContext context)
        { return Fold(context); }

        public IParseTree VisitDropLogfileGroup([NotNull] MySQLParser.DropLogfileGroupContext context)
        { return Fold(context); }

        public IParseTree VisitDropLogfileGroupOption([NotNull] MySQLParser.DropLogfileGroupOptionContext context)
        { return Fold(context); }

        public IParseTree VisitDropProcedure([NotNull] MySQLParser.DropProcedureContext context)
        { return Fold(context); }

        public IParseTree VisitDropResourceGroup([NotNull] MySQLParser.DropResourceGroupContext context)
        { return Fold(context); }

        public IParseTree VisitDropRole([NotNull] MySQLParser.DropRoleContext context)
        { return Fold(context); }

        public IParseTree VisitDropServer([NotNull] MySQLParser.DropServerContext context)
        { return Fold(context); }

        public IParseTree VisitDropSpatialReference([NotNull] MySQLParser.DropSpatialReferenceContext context)
        { return Fold(context); }

        public IParseTree VisitDropStatement([NotNull] MySQLParser.DropStatementContext context)
        { return Fold(context); }

        public IParseTree VisitDropTable([NotNull] MySQLParser.DropTableContext context)
        { return Fold(context); }

        public IParseTree VisitDropTableSpace([NotNull] MySQLParser.DropTableSpaceContext context)
        { return Fold(context); }

        public IParseTree VisitDropTrigger([NotNull] MySQLParser.DropTriggerContext context)
        { return Fold(context); }

        public IParseTree VisitDropUndoTablespace([NotNull] MySQLParser.DropUndoTablespaceContext context)
        { return Fold(context); }

        public IParseTree VisitDropUser([NotNull] MySQLParser.DropUserContext context)
        { return Fold(context); }

        public IParseTree VisitDropView([NotNull] MySQLParser.DropViewContext context)
        { return Fold(context); }

        public IParseTree VisitDuplicateAsQueryExpression([NotNull] MySQLParser.DuplicateAsQueryExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitElseExpression([NotNull] MySQLParser.ElseExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitElseStatement([NotNull] MySQLParser.ElseStatementContext context)
        { return Fold(context); }

        public IParseTree VisitEngineRef([NotNull] MySQLParser.EngineRefContext context)
        { return Fold(context); }

        public IParseTree VisitEqual([NotNull] MySQLParser.EqualContext context)
        { return Fold(context); }

        public IParseTree VisitErrorNode(IErrorNode node)
        { return Fold(node); }

        public IParseTree VisitEscapedTableReference([NotNull] MySQLParser.EscapedTableReferenceContext context)
        { return Fold(context); }

        public IParseTree VisitEventName([NotNull] MySQLParser.EventNameContext context)
        { return Fold(context); }

        public IParseTree VisitEventRef([NotNull] MySQLParser.EventRefContext context)
        { return Fold(context); }

        public IParseTree VisitExceptRoleList([NotNull] MySQLParser.ExceptRoleListContext context)
        { return Fold(context); }

        public IParseTree VisitExecuteStatement([NotNull] MySQLParser.ExecuteStatementContext context)
        { return Fold(context); }

        public IParseTree VisitExecuteVarList([NotNull] MySQLParser.ExecuteVarListContext context)
        { return Fold(context); }

        public IParseTree VisitExplainableStatement([NotNull] MySQLParser.ExplainableStatementContext context)
        { return Fold(context); }

        public IParseTree VisitExplainCommand([NotNull] MySQLParser.ExplainCommandContext context)
        { return Fold(context); }

        public IParseTree VisitExprAnd([NotNull] MySQLParser.ExprAndContext context)
        { return Fold(context); }

        public IParseTree VisitExprIs([NotNull] MySQLParser.ExprIsContext context)
        { return Fold(context); }

        public IParseTree VisitExprList([NotNull] MySQLParser.ExprListContext context)
        { return Fold(context); }

        public IParseTree VisitExprListWithParentheses([NotNull] MySQLParser.ExprListWithParenthesesContext context)
        { return Fold(context); }

        public IParseTree VisitExprNot([NotNull] MySQLParser.ExprNotContext context)
        { return Fold(context); }

        public IParseTree VisitExprOr([NotNull] MySQLParser.ExprOrContext context)
        { return Fold(context); }

        public IParseTree VisitExprWithParentheses([NotNull] MySQLParser.ExprWithParenthesesContext context)
        { return Fold(context); }

        public IParseTree VisitExprXor([NotNull] MySQLParser.ExprXorContext context)
        { return Fold(context); }

        public IParseTree VisitFieldDefinition([NotNull] MySQLParser.FieldDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitFieldIdentifier([NotNull] MySQLParser.FieldIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitFieldLength([NotNull] MySQLParser.FieldLengthContext context)
        { return Fold(context); }

        public IParseTree VisitFieldOptions([NotNull] MySQLParser.FieldOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitFieldOrVariableList([NotNull] MySQLParser.FieldOrVariableListContext context)
        { return Fold(context); }

        public IParseTree VisitFields([NotNull] MySQLParser.FieldsContext context)
        { return Fold(context); }

        public IParseTree VisitFieldsClause([NotNull] MySQLParser.FieldsClauseContext context)
        { return Fold(context); }

        public IParseTree VisitFieldTerm([NotNull] MySQLParser.FieldTermContext context)
        { return Fold(context); }

        public IParseTree VisitFilterDbList([NotNull] MySQLParser.FilterDbListContext context)
        { return Fold(context); }

        public IParseTree VisitFilterDbPairList([NotNull] MySQLParser.FilterDbPairListContext context)
        { return Fold(context); }

        public IParseTree VisitFilterDefinition([NotNull] MySQLParser.FilterDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitFilterStringList([NotNull] MySQLParser.FilterStringListContext context)
        { return Fold(context); }

        public IParseTree VisitFilterTableList([NotNull] MySQLParser.FilterTableListContext context)
        { return Fold(context); }

        public IParseTree VisitFilterTableRef([NotNull] MySQLParser.FilterTableRefContext context)
        { return Fold(context); }

        public IParseTree VisitFilterWildDbTableString([NotNull] MySQLParser.FilterWildDbTableStringContext context)
        { return Fold(context); }

        public IParseTree VisitFloatOptions([NotNull] MySQLParser.FloatOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitFlushOption([NotNull] MySQLParser.FlushOptionContext context)
        { return Fold(context); }

        public IParseTree VisitFlushTables([NotNull] MySQLParser.FlushTablesContext context)
        { return Fold(context); }

        public IParseTree VisitFlushTablesOptions([NotNull] MySQLParser.FlushTablesOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitFractionalPrecision([NotNull] MySQLParser.FractionalPrecisionContext context)
        { return Fold(context); }

        public IParseTree VisitFromClause([NotNull] MySQLParser.FromClauseContext context)
        { return Fold(context); }

        public IParseTree VisitFromOrIn([NotNull] MySQLParser.FromOrInContext context)
        { return Fold(context); }

        public IParseTree VisitFulltextIndexOption([NotNull] MySQLParser.FulltextIndexOptionContext context)
        { return Fold(context); }

        public IParseTree VisitFulltextOptions([NotNull] MySQLParser.FulltextOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitFunctionCall([NotNull] MySQLParser.FunctionCallContext context)
        { return Fold(context); }

        public IParseTree VisitFunctionName([NotNull] MySQLParser.FunctionNameContext context)
        { return Fold(context); }

        public IParseTree VisitFunctionParameter([NotNull] MySQLParser.FunctionParameterContext context)
        { return Fold(context); }

        public IParseTree VisitFunctionRef([NotNull] MySQLParser.FunctionRefContext context)
        { return Fold(context); }

        public IParseTree VisitGcolAttribute([NotNull] MySQLParser.GcolAttributeContext context)
        { return Fold(context); }

        public IParseTree VisitGeometryFunction([NotNull] MySQLParser.GeometryFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitGetDiagnostics([NotNull] MySQLParser.GetDiagnosticsContext context)
        { return Fold(context); }

        public IParseTree VisitGrant([NotNull] MySQLParser.GrantContext context)
        { return Fold(context); }

        public IParseTree VisitGrantAs([NotNull] MySQLParser.GrantAsContext context)
        { return Fold(context); }

        public IParseTree VisitGrantIdentifier([NotNull] MySQLParser.GrantIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitGrantOption([NotNull] MySQLParser.GrantOptionContext context)
        { return Fold(context); }

        public IParseTree VisitGrantOptions([NotNull] MySQLParser.GrantOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitGrantTargetList([NotNull] MySQLParser.GrantTargetListContext context)
        { return Fold(context); }

        public IParseTree VisitGroupByClause([NotNull] MySQLParser.GroupByClauseContext context)
        { return Fold(context); }

        public IParseTree VisitGroupingExpression([NotNull] MySQLParser.GroupingExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitGroupingOperation([NotNull] MySQLParser.GroupingOperationContext context)
        { return Fold(context); }

        public IParseTree VisitGroupList([NotNull] MySQLParser.GroupListContext context)
        { return Fold(context); }

        public IParseTree VisitGroupReplication([NotNull] MySQLParser.GroupReplicationContext context)
        { return Fold(context); }

        public IParseTree VisitHandlerCondition([NotNull] MySQLParser.HandlerConditionContext context)
        { return Fold(context); }

        public IParseTree VisitHandlerDeclaration([NotNull] MySQLParser.HandlerDeclarationContext context)
        { return Fold(context); }

        public IParseTree VisitHandlerReadOrScan([NotNull] MySQLParser.HandlerReadOrScanContext context)
        { return Fold(context); }

        public IParseTree VisitHandlerStatement([NotNull] MySQLParser.HandlerStatementContext context)
        { return Fold(context); }

        public IParseTree VisitHavingClause([NotNull] MySQLParser.HavingClauseContext context)
        { return Fold(context); }

        public IParseTree VisitHelpCommand([NotNull] MySQLParser.HelpCommandContext context)
        { return Fold(context); }

        public IParseTree VisitHistogram([NotNull] MySQLParser.HistogramContext context)
        { return Fold(context); }

        public IParseTree VisitIdentifier([NotNull] MySQLParser.IdentifierContext context)
        { return Fold(context); }


        public IParseTree VisitIdentifierKeyword([NotNull] MySQLParser.IdentifierKeywordContext context)
        { return Fold(context); }


        public IParseTree VisitIdentifierKeywordsAmbiguous1RolesAndLabels([NotNull] MySQLParser.IdentifierKeywordsAmbiguous1RolesAndLabelsContext context)
        { return Fold(context); }


        public IParseTree VisitIdentifierKeywordsAmbiguous2Labels([NotNull] MySQLParser.IdentifierKeywordsAmbiguous2LabelsContext context)
        { return Fold(context); }


        public IParseTree VisitIdentifierKeywordsAmbiguous3Roles([NotNull] MySQLParser.IdentifierKeywordsAmbiguous3RolesContext context)
        { return Fold(context); }

        public IParseTree VisitIdentifierKeywordsAmbiguous4SystemVariables([NotNull] MySQLParser.IdentifierKeywordsAmbiguous4SystemVariablesContext context)
        { return Fold(context); }

        public IParseTree VisitIdentifierKeywordsUnambiguous([NotNull] MySQLParser.IdentifierKeywordsUnambiguousContext context)
        { return Fold(context); }

        public IParseTree VisitIdentifierList([NotNull] MySQLParser.IdentifierListContext context)
        { return Fold(context); }

        public IParseTree VisitIdentifierListWithParentheses([NotNull] MySQLParser.IdentifierListWithParenthesesContext context)
        { return Fold(context); }

        public IParseTree VisitIdentList([NotNull] MySQLParser.IdentListContext context)
        { return Fold(context); }

        public IParseTree VisitIdentListArg([NotNull] MySQLParser.IdentListArgContext context)
        { return Fold(context); }

        public IParseTree VisitIfBody([NotNull] MySQLParser.IfBodyContext context)
        { return Fold(context); }

        public IParseTree VisitIfExists([NotNull] MySQLParser.IfExistsContext context)
        { return Fold(context); }

        public IParseTree VisitIfNotExists([NotNull] MySQLParser.IfNotExistsContext context)
        { return Fold(context); }

        public IParseTree VisitIfStatement([NotNull] MySQLParser.IfStatementContext context)
        { return Fold(context); }

        public IParseTree VisitImportStatement([NotNull] MySQLParser.ImportStatementContext context)
        { return Fold(context); }

        public IParseTree VisitInDb([NotNull] MySQLParser.InDbContext context)
        { return Fold(context); }

        public IParseTree VisitIndexHint([NotNull] MySQLParser.IndexHintContext context)
        { return Fold(context); }

        public IParseTree VisitIndexHintClause([NotNull] MySQLParser.IndexHintClauseContext context)
        { return Fold(context); }

        public IParseTree VisitIndexHintList([NotNull] MySQLParser.IndexHintListContext context)
        { return Fold(context); }

        public IParseTree VisitIndexHintType([NotNull] MySQLParser.IndexHintTypeContext context)
        { return Fold(context); }

        public IParseTree VisitIndexList([NotNull] MySQLParser.IndexListContext context)
        { return Fold(context); }

        public IParseTree VisitIndexListElement([NotNull] MySQLParser.IndexListElementContext context)
        { return Fold(context); }

        public IParseTree VisitIndexLockAndAlgorithm([NotNull] MySQLParser.IndexLockAndAlgorithmContext context)
        { return Fold(context); }

        public IParseTree VisitIndexName([NotNull] MySQLParser.IndexNameContext context)
        { return Fold(context); }

        public IParseTree VisitIndexNameAndType([NotNull] MySQLParser.IndexNameAndTypeContext context)
        { return Fold(context); }

        public IParseTree VisitIndexOption([NotNull] MySQLParser.IndexOptionContext context)
        { return Fold(context); }

        public IParseTree VisitIndexRef([NotNull] MySQLParser.IndexRefContext context)
        { return Fold(context); }

        public IParseTree VisitIndexType([NotNull] MySQLParser.IndexTypeContext context)
        { return Fold(context); }

        public IParseTree VisitIndexTypeClause([NotNull] MySQLParser.IndexTypeClauseContext context)
        { return Fold(context); }

        public IParseTree VisitInnerJoinType([NotNull] MySQLParser.InnerJoinTypeContext context)
        { return Fold(context); }

        public IParseTree VisitInsertFromConstructor([NotNull] MySQLParser.InsertFromConstructorContext context)
        { return Fold(context); }

        public IParseTree VisitInsertIdentifier([NotNull] MySQLParser.InsertIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitInsertLockOption([NotNull] MySQLParser.InsertLockOptionContext context)
        { return Fold(context); }

        public IParseTree VisitInsertQueryExpression([NotNull] MySQLParser.InsertQueryExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitInsertStatement([NotNull] MySQLParser.InsertStatementContext context)
        { return Fold(context); }

        public IParseTree VisitInsertUpdateList([NotNull] MySQLParser.InsertUpdateListContext context)
        { return Fold(context); }

        public IParseTree VisitInsertValues([NotNull] MySQLParser.InsertValuesContext context)
        { return Fold(context); }

        public IParseTree VisitInstallUninstallStatment([NotNull] MySQLParser.InstallUninstallStatmentContext context)
        { return Fold(context); }

        public IParseTree VisitInSumExpr([NotNull] MySQLParser.InSumExprContext context)
        { return Fold(context); }

        public IParseTree VisitInternalVariableName([NotNull] MySQLParser.InternalVariableNameContext context)
        { return Fold(context); }

        public IParseTree VisitInterval([NotNull] MySQLParser.IntervalContext context)
        { return Fold(context); }

        public IParseTree VisitIntervalTimeStamp([NotNull] MySQLParser.IntervalTimeStampContext context)
        { return Fold(context); }

        public IParseTree VisitIntoClause([NotNull] MySQLParser.IntoClauseContext context)
        { return Fold(context); }

        public IParseTree VisitIsolationLevel([NotNull] MySQLParser.IsolationLevelContext context)
        { return Fold(context); }

        public IParseTree VisitIterateStatement([NotNull] MySQLParser.IterateStatementContext context)
        { return Fold(context); }

        public IParseTree VisitJoinedTable([NotNull] MySQLParser.JoinedTableContext context)
        { return Fold(context); }

        public IParseTree VisitJsonFunction([NotNull] MySQLParser.JsonFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitJsonOperator([NotNull] MySQLParser.JsonOperatorContext context)
        { return Fold(context); }

        public IParseTree VisitJtColumn([NotNull] MySQLParser.JtColumnContext context)
        { return Fold(context); }

        public IParseTree VisitJtOnResponse([NotNull] MySQLParser.JtOnResponseContext context)
        { return Fold(context); }

        public IParseTree VisitKeyCacheList([NotNull] MySQLParser.KeyCacheListContext context)
        { return Fold(context); }

        public IParseTree VisitKeyCacheListOrParts([NotNull] MySQLParser.KeyCacheListOrPartsContext context)
        { return Fold(context); }

        public IParseTree VisitKeyList([NotNull] MySQLParser.KeyListContext context)
        { return Fold(context); }

        public IParseTree VisitKeyListVariants([NotNull] MySQLParser.KeyListVariantsContext context)
        { return Fold(context); }

        public IParseTree VisitKeyListWithExpression([NotNull] MySQLParser.KeyListWithExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitKeyOrIndex([NotNull] MySQLParser.KeyOrIndexContext context)
        { return Fold(context); }

        public IParseTree VisitKeyPart([NotNull] MySQLParser.KeyPartContext context)
        { return Fold(context); }

        public IParseTree VisitKeyPartOrExpression([NotNull] MySQLParser.KeyPartOrExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitKeyUsageElement([NotNull] MySQLParser.KeyUsageElementContext context)
        { return Fold(context); }

        public IParseTree VisitKeyUsageList([NotNull] MySQLParser.KeyUsageListContext context)
        { return Fold(context); }

        public IParseTree VisitLabel([NotNull] MySQLParser.LabelContext context)
        { return Fold(context); }

        public IParseTree VisitLabeledBlock([NotNull] MySQLParser.LabeledBlockContext context)
        { return Fold(context); }

        public IParseTree VisitLabeledControl([NotNull] MySQLParser.LabeledControlContext context)
        { return Fold(context); }

        public IParseTree VisitLabelIdentifier([NotNull] MySQLParser.LabelIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitLabelKeyword([NotNull] MySQLParser.LabelKeywordContext context)
        { return Fold(context); }

        public IParseTree VisitLabelRef([NotNull] MySQLParser.LabelRefContext context)
        { return Fold(context); }

        public IParseTree VisitLeadLagInfo([NotNull] MySQLParser.LeadLagInfoContext context)
        { return Fold(context); }

        public IParseTree VisitLeaveStatement([NotNull] MySQLParser.LeaveStatementContext context)
        { return Fold(context); }

        public IParseTree VisitLikeClause([NotNull] MySQLParser.LikeClauseContext context)
        { return Fold(context); }

        public IParseTree VisitLikeOrWhere([NotNull] MySQLParser.LikeOrWhereContext context)
        { return Fold(context); }

        public IParseTree VisitLimitClause([NotNull] MySQLParser.LimitClauseContext context)
        { return Fold(context); }

        public IParseTree VisitLimitOption([NotNull] MySQLParser.LimitOptionContext context)
        { return Fold(context); }

        public IParseTree VisitLimitOptions([NotNull] MySQLParser.LimitOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitLinesClause([NotNull] MySQLParser.LinesClauseContext context)
        { return Fold(context); }

        public IParseTree VisitLineTerm([NotNull] MySQLParser.LineTermContext context)
        { return Fold(context); }

        public IParseTree VisitLiteral([NotNull] MySQLParser.LiteralContext context)
        { return Fold(context); }

        public IParseTree VisitLoadDataFileTail([NotNull] MySQLParser.LoadDataFileTailContext context)
        { return Fold(context); }

        public IParseTree VisitLoadDataFileTargetList([NotNull] MySQLParser.LoadDataFileTargetListContext context)
        { return Fold(context); }

        public IParseTree VisitLoadStatement([NotNull] MySQLParser.LoadStatementContext context)
        { return Fold(context); }

        public IParseTree VisitLockedRowAction([NotNull] MySQLParser.LockedRowActionContext context)
        { return Fold(context); }

        public IParseTree VisitLockingClause([NotNull] MySQLParser.LockingClauseContext context)
        { return Fold(context); }

        public IParseTree VisitLockItem([NotNull] MySQLParser.LockItemContext context)
        { return Fold(context); }

        public IParseTree VisitLockOption([NotNull] MySQLParser.LockOptionContext context)
        { return Fold(context); }

        public IParseTree VisitLockStatement([NotNull] MySQLParser.LockStatementContext context)
        { return Fold(context); }

        public IParseTree VisitLockStrengh([NotNull] MySQLParser.LockStrenghContext context)
        { return Fold(context); }

        public IParseTree VisitLogfileGroupName([NotNull] MySQLParser.LogfileGroupNameContext context)
        { return Fold(context); }

        public IParseTree VisitLogfileGroupOption([NotNull] MySQLParser.LogfileGroupOptionContext context)
        { return Fold(context); }

        public IParseTree VisitLogfileGroupOptions([NotNull] MySQLParser.LogfileGroupOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitLogfileGroupRef([NotNull] MySQLParser.LogfileGroupRefContext context)
        { return Fold(context); }

        public IParseTree VisitLogType([NotNull] MySQLParser.LogTypeContext context)
        { return Fold(context); }

        public IParseTree VisitLoopBlock([NotNull] MySQLParser.LoopBlockContext context)
        { return Fold(context); }

        public IParseTree VisitLValueIdentifier([NotNull] MySQLParser.LValueIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitLValueKeyword([NotNull] MySQLParser.LValueKeywordContext context)
        { return Fold(context); }

        public IParseTree VisitMasterFileDef([NotNull] MySQLParser.MasterFileDefContext context)
        { return Fold(context); }

        public IParseTree VisitMasterOption([NotNull] MySQLParser.MasterOptionContext context)
        { return Fold(context); }

        public IParseTree VisitMasterResetOptions([NotNull] MySQLParser.MasterResetOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitNaturalJoinType([NotNull] MySQLParser.NaturalJoinTypeContext context)
        { return Fold(context); }

        public IParseTree VisitNchar([NotNull] MySQLParser.NcharContext context)
        { return Fold(context); }

        public IParseTree VisitNonBlocking([NotNull] MySQLParser.NonBlockingContext context)
        { return Fold(context); }

        public IParseTree VisitNot2Rule([NotNull] MySQLParser.Not2RuleContext context)
        { return Fold(context); }

        public IParseTree VisitNotRule([NotNull] MySQLParser.NotRuleContext context)
        { return Fold(context); }

        public IParseTree VisitNoWriteToBinLog([NotNull] MySQLParser.NoWriteToBinLogContext context)
        { return Fold(context); }

        public IParseTree VisitNullLiteral([NotNull] MySQLParser.NullLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitNullTreatment([NotNull] MySQLParser.NullTreatmentContext context)
        { return Fold(context); }

        public IParseTree VisitNumLiteral([NotNull] MySQLParser.NumLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitNvarchar([NotNull] MySQLParser.NvarcharContext context)
        { return Fold(context); }

        public IParseTree VisitOlapOption([NotNull] MySQLParser.OlapOptionContext context)
        { return Fold(context); }

        public IParseTree VisitOnEmpty([NotNull] MySQLParser.OnEmptyContext context)
        { return Fold(context); }

        public IParseTree VisitOnEmptyOrError([NotNull] MySQLParser.OnEmptyOrErrorContext context)
        { return Fold(context); }

        public IParseTree VisitOnError([NotNull] MySQLParser.OnErrorContext context)
        { return Fold(context); }

        public IParseTree VisitOnlineOption([NotNull] MySQLParser.OnlineOptionContext context)
        { return Fold(context); }

        public IParseTree VisitOnTypeTo([NotNull] MySQLParser.OnTypeToContext context)
        { return Fold(context); }

        public IParseTree VisitOptionType([NotNull] MySQLParser.OptionTypeContext context)
        { return Fold(context); }

        public IParseTree VisitOptionValue([NotNull] MySQLParser.OptionValueContext context)
        { return Fold(context); }

        public IParseTree VisitOptionValueFollowingOptionType([NotNull] MySQLParser.OptionValueFollowingOptionTypeContext context)
        { return Fold(context); }

        public IParseTree VisitOptionValueList([NotNull] MySQLParser.OptionValueListContext context)
        { return Fold(context); }

        public IParseTree VisitOptionValueNoOptionType([NotNull] MySQLParser.OptionValueNoOptionTypeContext context)
        { return Fold(context); }

        public IParseTree VisitOrderClause([NotNull] MySQLParser.OrderClauseContext context)
        { return Fold(context); }

        public IParseTree VisitOrderExpression([NotNull] MySQLParser.OrderExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitOrderList([NotNull] MySQLParser.OrderListContext context)
        { return Fold(context); }

        public IParseTree VisitOtherAdministrativeStatement([NotNull] MySQLParser.OtherAdministrativeStatementContext context)
        { return Fold(context); }

        public IParseTree VisitOuterJoinType([NotNull] MySQLParser.OuterJoinTypeContext context)
        { return Fold(context); }

        public IParseTree VisitParameterName([NotNull] MySQLParser.ParameterNameContext context)
        { return Fold(context); }

        public IParseTree VisitParentheses([NotNull] MySQLParser.ParenthesesContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionClause([NotNull] MySQLParser.PartitionClauseContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionDefHash([NotNull] MySQLParser.PartitionDefHashContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionDefinition([NotNull] MySQLParser.PartitionDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionDefinitions([NotNull] MySQLParser.PartitionDefinitionsContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionDefKey([NotNull] MySQLParser.PartitionDefKeyContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionDefRangeList([NotNull] MySQLParser.PartitionDefRangeListContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionDelete([NotNull] MySQLParser.PartitionDeleteContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionKeyAlgorithm([NotNull] MySQLParser.PartitionKeyAlgorithmContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionOption([NotNull] MySQLParser.PartitionOptionContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionValueItem([NotNull] MySQLParser.PartitionValueItemContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionValueItemListParen([NotNull] MySQLParser.PartitionValueItemListParenContext context)
        { return Fold(context); }

        public IParseTree VisitPartitionValuesIn([NotNull] MySQLParser.PartitionValuesInContext context)
        { return Fold(context); }

        public IParseTree VisitPlace([NotNull] MySQLParser.PlaceContext context)
        { return Fold(context); }

        public IParseTree VisitPluginRef([NotNull] MySQLParser.PluginRefContext context)
        { return Fold(context); }

        public IParseTree VisitPrecision([NotNull] MySQLParser.PrecisionContext context)
        { return Fold(context); }

        public IParseTree VisitPredicate([NotNull] MySQLParser.PredicateContext context)
        { return Fold(context); }

        public IParseTree VisitPredicateExprBetween([NotNull] MySQLParser.PredicateExprBetweenContext context)
        { return Fold(context); }

        public IParseTree VisitPredicateExprIn([NotNull] MySQLParser.PredicateExprInContext context)
        { return Fold(context); }

        public IParseTree VisitPredicateExprLike([NotNull] MySQLParser.PredicateExprLikeContext context)
        { return Fold(context); }

        public IParseTree VisitPredicateExprRegex([NotNull] MySQLParser.PredicateExprRegexContext context)
        { return Fold(context); }

        public IParseTree VisitPreloadKeys([NotNull] MySQLParser.PreloadKeysContext context)
        { return Fold(context); }

        public IParseTree VisitPreloadList([NotNull] MySQLParser.PreloadListContext context)
        { return Fold(context); }

        public IParseTree VisitPreloadTail([NotNull] MySQLParser.PreloadTailContext context)
        { return Fold(context); }

        public IParseTree VisitPreparedStatement([NotNull] MySQLParser.PreparedStatementContext context)
        { return Fold(context); }

        public IParseTree VisitPrimaryExprAllAny([NotNull] MySQLParser.PrimaryExprAllAnyContext context)
        { return Fold(context); }

        public IParseTree VisitPrimaryExprCompare([NotNull] MySQLParser.PrimaryExprCompareContext context)
        { return Fold(context); }

        public IParseTree VisitPrimaryExprIsNull([NotNull] MySQLParser.PrimaryExprIsNullContext context)
        { return Fold(context); }

        public IParseTree VisitPrimaryExprPredicate([NotNull] MySQLParser.PrimaryExprPredicateContext context)
        { return Fold(context); }

        public IParseTree VisitProcedureAnalyseClause([NotNull] MySQLParser.ProcedureAnalyseClauseContext context)
        { return Fold(context); }

        public IParseTree VisitProcedureName([NotNull] MySQLParser.ProcedureNameContext context)
        { return Fold(context); }

        public IParseTree VisitProcedureParameter([NotNull] MySQLParser.ProcedureParameterContext context)
        { return Fold(context); }

        public IParseTree VisitProcedureRef([NotNull] MySQLParser.ProcedureRefContext context)
        { return Fold(context); }

        public IParseTree VisitProfileType([NotNull] MySQLParser.ProfileTypeContext context)
        { return Fold(context); }

        public IParseTree VisitPureIdentifier([NotNull] MySQLParser.PureIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitQualifiedIdentifier([NotNull] MySQLParser.QualifiedIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitQuery([NotNull] MySQLParser.QueryContext context)
        { return Fold(context); }

        public IParseTree VisitQueryExpression([NotNull] MySQLParser.QueryExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitQueryExpressionBody([NotNull] MySQLParser.QueryExpressionBodyContext context)
        { return Fold(context); }

        public IParseTree VisitQueryExpressionOrParens([NotNull] MySQLParser.QueryExpressionOrParensContext context)
        { return Fold(context); }

        public IParseTree VisitQueryExpressionParens([NotNull] MySQLParser.QueryExpressionParensContext context)
        { return Fold(context); }

        public IParseTree VisitQuerySpecification([NotNull] MySQLParser.QuerySpecificationContext context)
        { return Fold(context); }

        public IParseTree VisitQuerySpecOption([NotNull] MySQLParser.QuerySpecOptionContext context)
        { return Fold(context); }

        public IParseTree VisitRealType([NotNull] MySQLParser.RealTypeContext context)
        { return Fold(context); }

        public IParseTree VisitReal_ulonglong_number([NotNull] MySQLParser.Real_ulonglong_numberContext context)
        { return Fold(context); }

        public IParseTree VisitReal_ulong_number([NotNull] MySQLParser.Real_ulong_numberContext context)
        { return Fold(context); }

        public IParseTree VisitReferences([NotNull] MySQLParser.ReferencesContext context)
        { return Fold(context); }

        public IParseTree VisitRemovePartitioning([NotNull] MySQLParser.RemovePartitioningContext context)
        { return Fold(context); }

        public IParseTree VisitRenamePair([NotNull] MySQLParser.RenamePairContext context)
        { return Fold(context); }

        public IParseTree VisitRenameTableStatement([NotNull] MySQLParser.RenameTableStatementContext context)
        { return Fold(context); }

        public IParseTree VisitRenameUser([NotNull] MySQLParser.RenameUserContext context)
        { return Fold(context); }

        public IParseTree VisitReorgPartitionRule([NotNull] MySQLParser.ReorgPartitionRuleContext context)
        { return Fold(context); }

        public IParseTree VisitRepairType([NotNull] MySQLParser.RepairTypeContext context)
        { return Fold(context); }

        public IParseTree VisitRepeatUntilBlock([NotNull] MySQLParser.RepeatUntilBlockContext context)
        { return Fold(context); }

        public IParseTree VisitReplacePassword([NotNull] MySQLParser.ReplacePasswordContext context)
        { return Fold(context); }

        public IParseTree VisitReplaceStatement([NotNull] MySQLParser.ReplaceStatementContext context)
        { return Fold(context); }

        public IParseTree VisitReplicationLoad([NotNull] MySQLParser.ReplicationLoadContext context)
        { return Fold(context); }

        public IParseTree VisitReplicationStatement([NotNull] MySQLParser.ReplicationStatementContext context)
        { return Fold(context); }

        public IParseTree VisitRequireClause([NotNull] MySQLParser.RequireClauseContext context)
        { return Fold(context); }

        public IParseTree VisitRequireList([NotNull] MySQLParser.RequireListContext context)
        { return Fold(context); }

        public IParseTree VisitRequireListElement([NotNull] MySQLParser.RequireListElementContext context)
        { return Fold(context); }

        public IParseTree VisitResetOption([NotNull] MySQLParser.ResetOptionContext context)
        { return Fold(context); }

        public IParseTree VisitResignalStatement([NotNull] MySQLParser.ResignalStatementContext context)
        { return Fold(context); }

        public IParseTree VisitResourceGroupEnableDisable([NotNull] MySQLParser.ResourceGroupEnableDisableContext context)
        { return Fold(context); }

        public IParseTree VisitResourceGroupManagement([NotNull] MySQLParser.ResourceGroupManagementContext context)
        { return Fold(context); }

        public IParseTree VisitResourceGroupPriority([NotNull] MySQLParser.ResourceGroupPriorityContext context)
        { return Fold(context); }

        public IParseTree VisitResourceGroupRef([NotNull] MySQLParser.ResourceGroupRefContext context)
        { return Fold(context); }

        public IParseTree VisitResourceGroupVcpuList([NotNull] MySQLParser.ResourceGroupVcpuListContext context)
        { return Fold(context); }

        public IParseTree VisitRestartServer([NotNull] MySQLParser.RestartServerContext context)
        { return Fold(context); }

        public IParseTree VisitRestrict([NotNull] MySQLParser.RestrictContext context)
        { return Fold(context); }

        public IParseTree VisitRetainCurrentPassword([NotNull] MySQLParser.RetainCurrentPasswordContext context)
        { return Fold(context); }

        public IParseTree VisitReturnStatement([NotNull] MySQLParser.ReturnStatementContext context)
        { return Fold(context); }

        public IParseTree VisitRevoke([NotNull] MySQLParser.RevokeContext context)
        { return Fold(context); }

        public IParseTree VisitRole([NotNull] MySQLParser.RoleContext context)
        { return Fold(context); }

        public IParseTree VisitRoleIdentifier([NotNull] MySQLParser.RoleIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitRoleIdentifierOrText([NotNull] MySQLParser.RoleIdentifierOrTextContext context)
        { return Fold(context); }

        public IParseTree VisitRoleKeyword([NotNull] MySQLParser.RoleKeywordContext context)
        { return Fold(context); }

        public IParseTree VisitRoleList([NotNull] MySQLParser.RoleListContext context)
        { return Fold(context); }

        public IParseTree VisitRoleOrIdentifierKeyword([NotNull] MySQLParser.RoleOrIdentifierKeywordContext context)
        { return Fold(context); }

        public IParseTree VisitRoleOrLabelKeyword([NotNull] MySQLParser.RoleOrLabelKeywordContext context)
        { return Fold(context); }

        public IParseTree VisitRoleOrPrivilege([NotNull] MySQLParser.RoleOrPrivilegeContext context)
        { return Fold(context); }

        public IParseTree VisitRoleOrPrivilegesList([NotNull] MySQLParser.RoleOrPrivilegesListContext context)
        { return Fold(context); }

        public IParseTree VisitRoleRef([NotNull] MySQLParser.RoleRefContext context)
        { return Fold(context); }

        public IParseTree VisitRoutineAlterOptions([NotNull] MySQLParser.RoutineAlterOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitRoutineCreateOption([NotNull] MySQLParser.RoutineCreateOptionContext context)
        { return Fold(context); }

        public IParseTree VisitRoutineOption([NotNull] MySQLParser.RoutineOptionContext context)
        { return Fold(context); }

        public IParseTree VisitRuntimeFunctionCall([NotNull] MySQLParser.RuntimeFunctionCallContext context)
        { return Fold(context); }

        public IParseTree VisitSavepointStatement([NotNull] MySQLParser.SavepointStatementContext context)
        { return Fold(context); }

        public IParseTree VisitSchedule([NotNull] MySQLParser.ScheduleContext context)
        { return Fold(context); }

        public IParseTree VisitSchemaIdentifierPair([NotNull] MySQLParser.SchemaIdentifierPairContext context)
        { return Fold(context); }

        public IParseTree VisitSchemaName([NotNull] MySQLParser.SchemaNameContext context)
        { return Fold(context); }

        public IParseTree VisitSchemaRef([NotNull] MySQLParser.SchemaRefContext context)
        { return Fold(context); }

        public IParseTree VisitSelectAlias([NotNull] MySQLParser.SelectAliasContext context)
        { return Fold(context); }

        public IParseTree VisitSelectItem([NotNull] MySQLParser.SelectItemContext context)
        { return Fold(context); }

        public IParseTree VisitSelectItemList([NotNull] MySQLParser.SelectItemListContext context)
        { return Fold(context); }

        public IParseTree VisitSelectOption([NotNull] MySQLParser.SelectOptionContext context)
        { return Fold(context); }

        public IParseTree VisitSelectStatement([NotNull] MySQLParser.SelectStatementContext context)
        { return Fold(context); }

        public IParseTree VisitSelectStatementWithInto([NotNull] MySQLParser.SelectStatementWithIntoContext context)
        { return Fold(context); }

        public IParseTree VisitServerIdList([NotNull] MySQLParser.ServerIdListContext context)
        { return Fold(context); }

        public IParseTree VisitServerName([NotNull] MySQLParser.ServerNameContext context)
        { return Fold(context); }

        public IParseTree VisitServerOption([NotNull] MySQLParser.ServerOptionContext context)
        { return Fold(context); }

        public IParseTree VisitServerOptions([NotNull] MySQLParser.ServerOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitServerRef([NotNull] MySQLParser.ServerRefContext context)
        { return Fold(context); }

        public IParseTree VisitSetExprOrDefault([NotNull] MySQLParser.SetExprOrDefaultContext context)
        { return Fold(context); }

        public IParseTree VisitSetPassword([NotNull] MySQLParser.SetPasswordContext context)
        { return Fold(context); }

        public IParseTree VisitSetResourceGroup([NotNull] MySQLParser.SetResourceGroupContext context)
        { return Fold(context); }

        public IParseTree VisitSetRole([NotNull] MySQLParser.SetRoleContext context)
        { return Fold(context); }

        public IParseTree VisitSetStatement([NotNull] MySQLParser.SetStatementContext context)
        { return Fold(context); }

        public IParseTree VisitSetSystemVariable([NotNull] MySQLParser.SetSystemVariableContext context)
        { return Fold(context); }

        public IParseTree VisitSetTransactionCharacteristic([NotNull] MySQLParser.SetTransactionCharacteristicContext context)
        { return Fold(context); }

        public IParseTree VisitSetVarIdentType([NotNull] MySQLParser.SetVarIdentTypeContext context)
        { return Fold(context); }

        public IParseTree VisitShowCommandType([NotNull] MySQLParser.ShowCommandTypeContext context)
        { return Fold(context); }

        public IParseTree VisitShowStatement([NotNull] MySQLParser.ShowStatementContext context)
        { return Fold(context); }

        public IParseTree VisitSignalAllowedExpr([NotNull] MySQLParser.SignalAllowedExprContext context)
        { return Fold(context); }

        public IParseTree VisitSignalInformationItem([NotNull] MySQLParser.SignalInformationItemContext context)
        { return Fold(context); }

        public IParseTree VisitSignalInformationItemName([NotNull] MySQLParser.SignalInformationItemNameContext context)
        { return Fold(context); }

        public IParseTree VisitSignalStatement([NotNull] MySQLParser.SignalStatementContext context)
        { return Fold(context); }

        public IParseTree VisitSignedLiteral([NotNull] MySQLParser.SignedLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprBinary([NotNull] MySQLParser.SimpleExprBinaryContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprCase([NotNull] MySQLParser.SimpleExprCaseContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprCast([NotNull] MySQLParser.SimpleExprCastContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprCollate([NotNull] MySQLParser.SimpleExprCollateContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprColumnRef([NotNull] MySQLParser.SimpleExprColumnRefContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprConcat([NotNull] MySQLParser.SimpleExprConcatContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprConvert([NotNull] MySQLParser.SimpleExprConvertContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprConvertUsing([NotNull] MySQLParser.SimpleExprConvertUsingContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprDefault([NotNull] MySQLParser.SimpleExprDefaultContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprFunction([NotNull] MySQLParser.SimpleExprFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprGroupingOperation([NotNull] MySQLParser.SimpleExprGroupingOperationContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprInterval([NotNull] MySQLParser.SimpleExprIntervalContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprList([NotNull] MySQLParser.SimpleExprListContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprLiteral([NotNull] MySQLParser.SimpleExprLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprMatch([NotNull] MySQLParser.SimpleExprMatchContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprNot([NotNull] MySQLParser.SimpleExprNotContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprOdbc([NotNull] MySQLParser.SimpleExprOdbcContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprParamMarker([NotNull] MySQLParser.SimpleExprParamMarkerContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprRuntimeFunction([NotNull] MySQLParser.SimpleExprRuntimeFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprSubQuery([NotNull] MySQLParser.SimpleExprSubQueryContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprSum([NotNull] MySQLParser.SimpleExprSumContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprUnary([NotNull] MySQLParser.SimpleExprUnaryContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprValues([NotNull] MySQLParser.SimpleExprValuesContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprVariable([NotNull] MySQLParser.SimpleExprVariableContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprWindowingFunction([NotNull] MySQLParser.SimpleExprWindowingFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleExprWithParentheses([NotNull] MySQLParser.SimpleExprWithParenthesesContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleIdentifier([NotNull] MySQLParser.SimpleIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleLimitClause([NotNull] MySQLParser.SimpleLimitClauseContext context)
        { return Fold(context); }

        public IParseTree VisitSimpleStatement([NotNull] MySQLParser.SimpleStatementContext context)
        { return Fold(context); }

        public IParseTree VisitSingleTable([NotNull] MySQLParser.SingleTableContext context)
        { return Fold(context); }

        public IParseTree VisitSingleTableParens([NotNull] MySQLParser.SingleTableParensContext context)
        { return Fold(context); }

        public IParseTree VisitSizeNumber([NotNull] MySQLParser.SizeNumberContext context)
        { return Fold(context); }

        public IParseTree VisitSlave([NotNull] MySQLParser.SlaveContext context)
        { return Fold(context); }

        public IParseTree VisitSlaveConnectionOptions([NotNull] MySQLParser.SlaveConnectionOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitSlaveThreadOption([NotNull] MySQLParser.SlaveThreadOptionContext context)
        { return Fold(context); }

        public IParseTree VisitSlaveThreadOptions([NotNull] MySQLParser.SlaveThreadOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitSlaveUntilOptions([NotNull] MySQLParser.SlaveUntilOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitSpatialIndexOption([NotNull] MySQLParser.SpatialIndexOptionContext context)
        { return Fold(context); }

        public IParseTree VisitSpCondition([NotNull] MySQLParser.SpConditionContext context)
        { return Fold(context); }

        public IParseTree VisitSpDeclaration([NotNull] MySQLParser.SpDeclarationContext context)
        { return Fold(context); }

        public IParseTree VisitSpDeclarations([NotNull] MySQLParser.SpDeclarationsContext context)
        { return Fold(context); }

        public IParseTree VisitSqlstate([NotNull] MySQLParser.SqlstateContext context)
        { return Fold(context); }

        public IParseTree VisitSrsAttribute([NotNull] MySQLParser.SrsAttributeContext context)
        { return Fold(context); }

        public IParseTree VisitSsl([NotNull] MySQLParser.SslContext context)
        { return Fold(context); }

        public IParseTree VisitStandaloneAlterCommands([NotNull] MySQLParser.StandaloneAlterCommandsContext context)
        { return Fold(context); }

        public IParseTree VisitStandardFloatOptions([NotNull] MySQLParser.StandardFloatOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitStatementInformationItem([NotNull] MySQLParser.StatementInformationItemContext context)
        { return Fold(context); }

        public IParseTree VisitStorageMedia([NotNull] MySQLParser.StorageMediaContext context)
        { return Fold(context); }

        public IParseTree VisitStringList([NotNull] MySQLParser.StringListContext context)
        { return Fold(context); }

        public IParseTree VisitSubpartitionDefinition([NotNull] MySQLParser.SubpartitionDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitSubPartitions([NotNull] MySQLParser.SubPartitionsContext context)
        { return Fold(context); }

        public IParseTree VisitSubquery([NotNull] MySQLParser.SubqueryContext context)
        { return Fold(context); }

        public IParseTree VisitSubstringFunction([NotNull] MySQLParser.SubstringFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitSumExpr([NotNull] MySQLParser.SumExprContext context)
        { return Fold(context); }

        public IParseTree VisitSystemVariable([NotNull] MySQLParser.SystemVariableContext context)
        { return Fold(context); }

        public IParseTree VisitTableAdministrationStatement([NotNull] MySQLParser.TableAdministrationStatementContext context)
        { return Fold(context); }

        public IParseTree VisitTableAlias([NotNull] MySQLParser.TableAliasContext context)
        { return Fold(context); }

        public IParseTree VisitTableAliasRefList([NotNull] MySQLParser.TableAliasRefListContext context)
        { return Fold(context); }

        public IParseTree VisitTableConstraintDef([NotNull] MySQLParser.TableConstraintDefContext context)
        { return Fold(context); }

        public IParseTree VisitTableElement([NotNull] MySQLParser.TableElementContext context)
        { return Fold(context); }

        public IParseTree VisitTableElementList([NotNull] MySQLParser.TableElementListContext context)
        { return Fold(context); }

        public IParseTree VisitTableFactor([NotNull] MySQLParser.TableFactorContext context)
        { return Fold(context); }

        public IParseTree VisitTableFunction([NotNull] MySQLParser.TableFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitTableName([NotNull] MySQLParser.TableNameContext context)
        { return Fold(context); }

        public IParseTree VisitTableRef([NotNull] MySQLParser.TableRefContext context)
        { return Fold(context); }

        public IParseTree VisitTableReference([NotNull] MySQLParser.TableReferenceContext context)
        { return Fold(context); }

        public IParseTree VisitTableReferenceList([NotNull] MySQLParser.TableReferenceListContext context)
        { return Fold(context); }

        public IParseTree VisitTableReferenceListParens([NotNull] MySQLParser.TableReferenceListParensContext context)
        { return Fold(context); }

        public IParseTree VisitTableRefList([NotNull] MySQLParser.TableRefListContext context)
        { return Fold(context); }

        public IParseTree VisitTableRefWithWildcard([NotNull] MySQLParser.TableRefWithWildcardContext context)
        { return Fold(context); }

        public IParseTree VisitTablespaceName([NotNull] MySQLParser.TablespaceNameContext context)
        { return Fold(context); }

        public IParseTree VisitTablespaceOption([NotNull] MySQLParser.TablespaceOptionContext context)
        { return Fold(context); }

        public IParseTree VisitTablespaceOptions([NotNull] MySQLParser.TablespaceOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitTablespaceRef([NotNull] MySQLParser.TablespaceRefContext context)
        { return Fold(context); }

        public IParseTree VisitTableWild([NotNull] MySQLParser.TableWildContext context)
        { return Fold(context); }

        public IParseTree VisitTemporalLiteral([NotNull] MySQLParser.TemporalLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitTerminal(ITerminalNode node)
        { return Fold(node); }

        public IParseTree VisitTernaryOption([NotNull] MySQLParser.TernaryOptionContext context)
        { return Fold(context); }

        public IParseTree VisitTextLiteral([NotNull] MySQLParser.TextLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitTextOrIdentifier([NotNull] MySQLParser.TextOrIdentifierContext context)
        { return Fold(context); }

        public IParseTree VisitTextString([NotNull] MySQLParser.TextStringContext context)
        { return Fold(context); }

        public IParseTree VisitTextStringHash([NotNull] MySQLParser.TextStringHashContext context)
        { return Fold(context); }

        public IParseTree VisitTextStringLiteral([NotNull] MySQLParser.TextStringLiteralContext context)
        { return Fold(context); }

        public IParseTree VisitTextStringLiteralList([NotNull] MySQLParser.TextStringLiteralListContext context)
        { return Fold(context); }

        public IParseTree VisitTextStringNoLinebreak([NotNull] MySQLParser.TextStringNoLinebreakContext context)
        { return Fold(context); }

        public IParseTree VisitThenExpression([NotNull] MySQLParser.ThenExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitThenStatement([NotNull] MySQLParser.ThenStatementContext context)
        { return Fold(context); }

        public IParseTree VisitThreadIdList([NotNull] MySQLParser.ThreadIdListContext context)
        { return Fold(context); }

        public IParseTree VisitTimeFunctionParameters([NotNull] MySQLParser.TimeFunctionParametersContext context)
        { return Fold(context); }

        public IParseTree VisitTransactionCharacteristic([NotNull] MySQLParser.TransactionCharacteristicContext context)
        { return Fold(context); }

        public IParseTree VisitTransactionOrLockingStatement([NotNull] MySQLParser.TransactionOrLockingStatementContext context)
        { return Fold(context); }

        public IParseTree VisitTransactionStatement([NotNull] MySQLParser.TransactionStatementContext context)
        { return Fold(context); }

        public IParseTree VisitTriggerFollowsPrecedesClause([NotNull] MySQLParser.TriggerFollowsPrecedesClauseContext context)
        { return Fold(context); }

        public IParseTree VisitTriggerName([NotNull] MySQLParser.TriggerNameContext context)
        { return Fold(context); }

        public IParseTree VisitTriggerRef([NotNull] MySQLParser.TriggerRefContext context)
        { return Fold(context); }

        public IParseTree VisitTrimFunction([NotNull] MySQLParser.TrimFunctionContext context)
        { return Fold(context); }

        public IParseTree VisitTruncateTableStatement([NotNull] MySQLParser.TruncateTableStatementContext context)
        { return Fold(context); }

        public IParseTree VisitTsDataFile([NotNull] MySQLParser.TsDataFileContext context)
        { return Fold(context); }

        public IParseTree VisitTsDataFileName([NotNull] MySQLParser.TsDataFileNameContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionAutoextendSize([NotNull] MySQLParser.TsOptionAutoextendSizeContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionComment([NotNull] MySQLParser.TsOptionCommentContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionEncryption([NotNull] MySQLParser.TsOptionEncryptionContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionEngine([NotNull] MySQLParser.TsOptionEngineContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionExtentSize([NotNull] MySQLParser.TsOptionExtentSizeContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionFileblockSize([NotNull] MySQLParser.TsOptionFileblockSizeContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionInitialSize([NotNull] MySQLParser.TsOptionInitialSizeContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionMaxSize([NotNull] MySQLParser.TsOptionMaxSizeContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionNodegroup([NotNull] MySQLParser.TsOptionNodegroupContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionUndoRedoBufferSize([NotNull] MySQLParser.TsOptionUndoRedoBufferSizeContext context)
        { return Fold(context); }

        public IParseTree VisitTsOptionWait([NotNull] MySQLParser.TsOptionWaitContext context)
        { return Fold(context); }

        public IParseTree VisitTypeDatetimePrecision([NotNull] MySQLParser.TypeDatetimePrecisionContext context)
        { return Fold(context); }

        public IParseTree VisitTypeWithOptCollate([NotNull] MySQLParser.TypeWithOptCollateContext context)
        { return Fold(context); }

        public IParseTree VisitUdfExpr([NotNull] MySQLParser.UdfExprContext context)
        { return Fold(context); }

        public IParseTree VisitUdfExprList([NotNull] MySQLParser.UdfExprListContext context)
        { return Fold(context); }

        public IParseTree VisitUdfName([NotNull] MySQLParser.UdfNameContext context)
        { return Fold(context); }

        public IParseTree VisitUlonglong_number([NotNull] MySQLParser.Ulonglong_numberContext context)
        { return Fold(context); }

        public IParseTree VisitUlong_number([NotNull] MySQLParser.Ulong_numberContext context)
        { return Fold(context); }

        public IParseTree VisitUndoTableSpaceOption([NotNull] MySQLParser.UndoTableSpaceOptionContext context)
        { return Fold(context); }

        public IParseTree VisitUndoTableSpaceOptions([NotNull] MySQLParser.UndoTableSpaceOptionsContext context)
        { return Fold(context); }

        public IParseTree VisitUnicode([NotNull] MySQLParser.UnicodeContext context)
        { return Fold(context); }

        public IParseTree VisitUnionOption([NotNull] MySQLParser.UnionOptionContext context)
        { return Fold(context); }

        public IParseTree VisitUnlabeledBlock([NotNull] MySQLParser.UnlabeledBlockContext context)
        { return Fold(context); }

        public IParseTree VisitUnlabeledControl([NotNull] MySQLParser.UnlabeledControlContext context)
        { return Fold(context); }

        public IParseTree VisitUpdateElement([NotNull] MySQLParser.UpdateElementContext context)
        { return Fold(context); }

        public IParseTree VisitUpdateList([NotNull] MySQLParser.UpdateListContext context)
        { return Fold(context); }

        public IParseTree VisitUpdateStatement([NotNull] MySQLParser.UpdateStatementContext context)
        { return Fold(context); }

        public IParseTree VisitUseCommand([NotNull] MySQLParser.UseCommandContext context)
        { return Fold(context); }

        public IParseTree VisitUsePartition([NotNull] MySQLParser.UsePartitionContext context)
        { return Fold(context); }

        public IParseTree VisitUser([NotNull] MySQLParser.UserContext context)
        { return Fold(context); }

        public IParseTree VisitUserList([NotNull] MySQLParser.UserListContext context)
        { return Fold(context); }

        public IParseTree VisitUserVariable([NotNull] MySQLParser.UserVariableContext context)
        { return Fold(context); }

        public IParseTree VisitUtilityStatement([NotNull] MySQLParser.UtilityStatementContext context)
        { return Fold(context); }

        public IParseTree VisitValueList([NotNull] MySQLParser.ValueListContext context)
        { return Fold(context); }

        public IParseTree VisitValues([NotNull] MySQLParser.ValuesContext context)
        { return Fold(context); }

        public IParseTree VisitVarchar([NotNull] MySQLParser.VarcharContext context)
        { return Fold(context); }

        public IParseTree VisitVariable([NotNull] MySQLParser.VariableContext context)
        { return Fold(context); }

        public IParseTree VisitVariableDeclaration([NotNull] MySQLParser.VariableDeclarationContext context)
        { return Fold(context); }

        public IParseTree VisitVarIdentType([NotNull] MySQLParser.VarIdentTypeContext context)
        { return Fold(context); }

        public IParseTree VisitVcpuNumOrRange([NotNull] MySQLParser.VcpuNumOrRangeContext context)
        { return Fold(context); }

        public IParseTree VisitVersionedRequireClause([NotNull] MySQLParser.VersionedRequireClauseContext context)
        { return Fold(context); }

        public IParseTree VisitViewAlgorithm([NotNull] MySQLParser.ViewAlgorithmContext context)
        { return Fold(context); }

        public IParseTree VisitViewCheckOption([NotNull] MySQLParser.ViewCheckOptionContext context)
        { return Fold(context); }

        public IParseTree VisitViewName([NotNull] MySQLParser.ViewNameContext context)
        { return Fold(context); }

        public IParseTree VisitViewRef([NotNull] MySQLParser.ViewRefContext context)
        { return Fold(context); }

        public IParseTree VisitViewRefList([NotNull] MySQLParser.ViewRefListContext context)
        { return Fold(context); }

        public IParseTree VisitViewReplaceOrAlgorithm([NotNull] MySQLParser.ViewReplaceOrAlgorithmContext context)
        { return Fold(context); }

        public IParseTree VisitViewSelect([NotNull] MySQLParser.ViewSelectContext context)
        { return Fold(context); }

        public IParseTree VisitViewSuid([NotNull] MySQLParser.ViewSuidContext context)
        { return Fold(context); }

        public IParseTree VisitViewTail([NotNull] MySQLParser.ViewTailContext context)
        { return Fold(context); }

        public IParseTree VisitVisibility([NotNull] MySQLParser.VisibilityContext context)
        { return Fold(context); }

        public IParseTree VisitWeightStringLevelListItem([NotNull] MySQLParser.WeightStringLevelListItemContext context)
        { return Fold(context); }

        public IParseTree VisitWeightStringLevels([NotNull] MySQLParser.WeightStringLevelsContext context)
        { return Fold(context); }

        public IParseTree VisitWhenExpression([NotNull] MySQLParser.WhenExpressionContext context)
        { return Fold(context); }

        public IParseTree VisitWhereClause([NotNull] MySQLParser.WhereClauseContext context)
        { return Fold(context); }

        public IParseTree VisitWhileDoBlock([NotNull] MySQLParser.WhileDoBlockContext context)
        { return Fold(context); }

        public IParseTree VisitWindowClause([NotNull] MySQLParser.WindowClauseContext context)
        { return Fold(context); }

        public IParseTree VisitWindowDefinition([NotNull] MySQLParser.WindowDefinitionContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameBetween([NotNull] MySQLParser.WindowFrameBetweenContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameBound([NotNull] MySQLParser.WindowFrameBoundContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameClause([NotNull] MySQLParser.WindowFrameClauseContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameExclusion([NotNull] MySQLParser.WindowFrameExclusionContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameExtent([NotNull] MySQLParser.WindowFrameExtentContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameStart([NotNull] MySQLParser.WindowFrameStartContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFrameUnits([NotNull] MySQLParser.WindowFrameUnitsContext context)
        { return Fold(context); }

        public IParseTree VisitWindowFunctionCall([NotNull] MySQLParser.WindowFunctionCallContext context)
        { return Fold(context); }

        public IParseTree VisitWindowingClause([NotNull] MySQLParser.WindowingClauseContext context)
        { return Fold(context); }

        public IParseTree VisitWindowName([NotNull] MySQLParser.WindowNameContext context)
        { return Fold(context); }

        public IParseTree VisitWindowSpec([NotNull] MySQLParser.WindowSpecContext context)
        { return Fold(context); }

        public IParseTree VisitWindowSpecDetails([NotNull] MySQLParser.WindowSpecDetailsContext context)
        { return Fold(context); }

        public IParseTree VisitWithClause([NotNull] MySQLParser.WithClauseContext context)
        { return Fold(context); }

        public IParseTree VisitWithRoles([NotNull] MySQLParser.WithRolesContext context)
        { return Fold(context); }

        public IParseTree VisitWithValidation([NotNull] MySQLParser.WithValidationContext context)
        { return Fold(context); }

        public IParseTree VisitWsNumCodepoints([NotNull] MySQLParser.WsNumCodepointsContext context)
        { return Fold(context); }

        public IParseTree VisitXaConvert([NotNull] MySQLParser.XaConvertContext context)
        { return Fold(context); }

        public IParseTree VisitXaStatement([NotNull] MySQLParser.XaStatementContext context)
        { return Fold(context); }

        public IParseTree VisitXid([NotNull] MySQLParser.XidContext context)
        { return Fold(context); }

        public IParseTree VisitXmlRowsIdentifiedBy([NotNull] MySQLParser.XmlRowsIdentifiedByContext context)
        { return Fold(context); }
    }
}