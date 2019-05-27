using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.PublicationDB
{
    public enum PublicationType
    {
        Монографії,
        Підручники,
        Навчальні_Посібники,
        Статті,
        Інші_Наукові_Видання,
        Тези_Доповідей_На_Конференціях,
        Патенти,
        Публікації_З_Студентами,
        Статті_В_Закордонних_Виданнях,
        Статті_В_Фахових_Виданнях_України,
        Статті_В_Інших_Виданнях_України,
    }
}