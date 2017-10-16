using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace WEE_API.Common
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString GetDisplayNameTUCM<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression
        )
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(value);
        }
 
    }
}