﻿using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using LinqToDB.Expressions;
	using SqlQuery;

	class WhereExpressionBuilder : ExpressionBuilderNew
	{
		public static QueryExpression Translate(QueryBuilder builder, QueryExpression qe, MethodCallExpression expression)
		{
			return qe.AddBuilder(new WhereExpressionBuilder(builder, expression));
		}

		WhereExpressionBuilder(QueryBuilder queryBuilder, MethodCallExpression expression)
			: base(queryBuilder, expression)
		{
			_isHaving  = expression.Method.Name == "Having";
		}

		bool _isHaving;

		public override Expression BuildQueryExpression<T>(QueryBuilder<T> builder)
		{
			return Prev.BuildQueryExpression(builder);
		}

		public override void BuildQuery<T>(QueryBuilder<T> builder)
		{
			Prev.BuildQuery(builder);
		}

		public override SelectQuery BuildSql<T>(QueryBuilder<T> builder, SelectQuery selectQuery)
		{
			var methodCall = (MethodCallExpression)Expression;
			var condition  = (LambdaExpression)methodCall.Arguments[1].Unwrap();

			builder.Builders.Add(condition.Parameters[0], this);

//			var result    = builder.BuildWhere(buildInfo.Parent, sequence, condition, !isHaving, isHaving);
//
//			result.SetAlias(condition.Parameters[0].Name);

			builder.Builders.Remove(condition.Parameters[0]);

			throw new NotImplementedException();
		}
	}
}