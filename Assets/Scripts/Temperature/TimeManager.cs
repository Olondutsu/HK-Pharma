    using UnityEngine;

    public class TimeManager : MonoBehaviour
   {
        public int year = 1; // L'année de départ est 1792
        public int month = 1;
        public int day = 1;
        public int decade = 1; // Décade de l'année
        private int dayOfWeek = 1; // Jour de la semaine

        private int daysInMonth = 30;
        private int daysInYearCommon = 365; // Année commune
        private int daysInYearLeap = 366;   // Année bissextile

        private int daysOfLastMonthLeap = 6;

        private int daysOfLastMonthCommon = 5;


        //Saisons
        public bool isAutumn = false;

        public bool isWinter = false;

        public bool isSpring = false;

       public bool isSummer = false;

        private bool IsLeapYear(int year){

            return (year - 3) % 4 == 0;
        }

        private void Update(){

        }

        public void TimeUpdate(){

            if (month == 13 && (dayOfWeek <= (IsLeapYear(year) ? daysOfLastMonthLeap : daysOfLastMonthCommon))){
                daysInMonth = IsLeapYear(year) ? 6 : 5;
            }
            
            if (day >= daysInMonth){
                
                if (month == 13 && day < (IsLeapYear(year) ? daysInYearLeap : daysInYearCommon)){

                    year++;
                    month = 1;
                    day = 1;
                    decade = 1;
                    dayOfWeek = 1;
                }

                else{
                
                // Si day est supérieur a DaysInMonth sans que ce soit le 13e mois

                month++;
                day = 1;

                    // Ajouter dayOfWeek tant qu'on atteint pas 10 (dans le mois ?)
                    if (dayOfWeek < 10) dayOfWeek++;
                    
                    // Si on arrive au 10e jour
                    else{
                        // Tant que Decade est sous 37 on ajoute 1 Decade (mais il devriat l'être à chaque fois)
                        dayOfWeek = 1;
                        if (decade < 37) decade++;
                        else decade = 1;
                    }
                }
            }
            else{

                // Si le nombre de jour est inférieur au nombre de jours dans le mois

                day++;
                if (dayOfWeek < 10) dayOfWeek++;
                else{
                    dayOfWeek = 1;
                    if (decade < 37) decade++;
                    else decade = 1;
                }
            }

            UpdateGameLogic();
        }
        
        public void SeasonHandler(){

            if (month == 1){ // Sept

                isSummer = true;
            }

            if (month == 2){ // Oct

                isSummer = false;
                isAutumn = true;
            }

            if (month == 3){ // Nov

                isWinter = true;
                isAutumn = false;
            }

            if (month == 4){ // Dec

                isWinter= true;
            }
            
            if (month == 5){ // Jan

                isWinter = true;
            }

            if (month == 6){ // Fev
                
                isWinter= true;
            }

            if (month == 7){ //Mars

                isWinter = false;
                isSpring = true;
            }

            if (month == 8){ // Avril

                isSpring = true;
            }

            if (month == 9){ // Mai

                isSpring = true;
            }

            if (month == 9){ // Juin

                isSpring = false;
                isSummer = true;
            }

            if (month == 10){ // Juill

                isSummer = true;
            }

            if (month == 11){ //Aout

                isSummer = true;
            }

            if (month == 12){ // Sept

                isSummer = true;
            }

            if (month == 13){ // Sept

                isSummer = true;
            }
    
        }

        private void UpdateGameLogic(){

            string monthName = GetMonthName(month);
            string dayName = GetDayName(dayOfWeek);
            Debug.Log("Année: " + year + " Mois: " + monthName + " Jour: " + dayName + "Decade :" + decade);
        }

        private string GetMonthName(int month)
        {

            string[] monthNames = {
                "Vendémiaire", "Brumaire", "Frimaire", "Nivôse", "Pluviôse", "Ventôse", "Germinal", "Floréal", "Prairial", "Messidor", "Thermidor", "Fructidor", "Complementary Days"
            };
            return monthNames[month - 1];
        }

        private string GetDayName(int dayOfWeek){

            string[] dayNames = {
                "Primidi", "Duodi", "Tridi", "Quartidi", "Quintidi", "Sextidi", "Septidi", "Octidi", "Nonidi", "Décadi"
            };

            return dayNames[dayOfWeek - 1];
        }

}