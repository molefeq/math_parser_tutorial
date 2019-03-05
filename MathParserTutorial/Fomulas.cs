using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTutorial
{
    public static class Fomulas
    {
        public static string GROSS_REMERATION_FORMULA = "basic_salary + total_employer_contributions + employer_medical_aid_contribution + (travel_allowance * travel_allowance_inclusion_percentage) + total_other_allowances";
        public static string ANNUALISED_FORMULA = "payment_frequency * amount";
        public static string RETIREMENT_FUND_CONTRIBUTION_FORMULA = "employer_contribution + employee_contribution";
        public static string RETIREMENT_FUND_CONTRIBUTION_LIMIT_FORMULA = "(limit_percentage/100) * annual_retirement_funds_contribution";
        public static string RETIREMENT_FUND_CONTRIBUTION_ABSOLUTE_LIMIT_FORMULA = "min(max_ra_amount, limit_ra_amount)";
        public static string RETIREMENT_FUND_CONTRIBUTION_DEDUCTION_FORMULA = "min(annual_ra_amount, limit_ra_amount, absolute_limit_ra_limit)";
        public static string TAXABLE_INCOME_FORMULA = "annual_gross_renumeration - retirement_funds_contribution_deduction";
        public static string FIRST_DEPENDANT_MEDICAL_AID_CREDIT_FORMULA = "first_dependent_medical_aid_credit * (if(number_of_dependents > 0, 1, 0))";
        public static string OTHER_DEPENDANT_MEDICAL_AID_CREDIT_FORMULA = "max(other_dependent_medical_aid_credit * (number_of_dependents-1), 0)";
        public static string MEDICAL_AID_CREDIT_FORMULA = "additional_depandant_medical_aid_tax_credit + main_member_medical_aid_credit + first_dependent_medical_aid_credit";
        public static string ANNUAL_TAX_AMOUNT_FORMULA = "sliding_scale + (taxable_income - min_taxable_amount)*(tax_percentage/100) - tax_rebate";
        public static string UIF_FORMULA = "min(gross_amount*0.01, max_uif_amount)";
        public static string TAKE_HOME_FORMULA = "gross_income-uif_amount-taxable_amount";

    }
}
