using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
	public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
	{
        return visits.OrderBy(x => x.DateTime).GroupBy(x => x.UserId)
                .SelectMany(group => group.Bigrams().Where(bigram => bigram.Item1.SlideType == slideType))
                .Select(bigram => bigram.Item2.DateTime.Subtract(bigram.Item1.DateTime).TotalMinutes)
                .Where(minutes => minutes >= 1 && minutes <= 120).DefaultIfEmpty()
                .Median();
    }
}

/*В файле StatisticsTask реализуйте метод GetMedianTimePerSlide. Он должен работать так.

Обозначим T(U, S) время между посещением пользователем U слайда S и ближайшим следующим посещением 
тем же пользователем U какого-то другого слайда S2 != S.

T(U, S) можно считать примерной оценкой того, сколько времени пользователь U провел на слайде S.
Метод должен для указанного типа слайда, считать медиану значений T(U, S) по всем пользователям и всем слайдам этого типа.
Нужно игнорировать значения меньшие 1 минуты и большие 2 часов при расчете медианы.
Гарантируется, что в тестах и реальных данных отсутствуют записи, когда определенный пользователь 
заходит на один и тот же слайд более одного раза.
Время нужно возвращать в минутах.
Воспользуйтесь реализованными ранее методами Bigrams и Median*/