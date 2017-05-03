﻿using Framwork;
using SimpleMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper
{
    public class ExpressionRepository<T> where T:new()
    {
        private AbstractMapper<T> mapper;
        public ExpressionRepository() {
            mapper = new AbstractMapper<T>();
        }
        public T Get(Expression<Func<T,bool>> exrpression) {
            string where = new QueryTranslator().TranslateWhere(exrpression);
            return mapper.Find(where);
        }

        public int Update(T model, Expression<Func<T, bool>> expression)
        {
            string where = new QueryTranslator().TranslateWhere(expression);
            return mapper.Update(model, where);
        }

        public int Insert(T model) {
            return mapper.Insert(model);
        }

        public int Detele(Expression<Func<T, bool>> expression)
        {
            string where = GetWhere(expression);
            return mapper.Delete(where);
        }

        public string GetWhere(Expression<Func<T, bool>> expression)
        {
            return new QueryTranslator().TranslateWhere(expression);
        }
        public void BeginTransaction() {
            mapper.CreateTransaction();
        }
        public void CommitTransaction() {
            mapper.CommitTransaction();
        }
    }
}
