using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;

namespace MathParserTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var basicSalary = 57460d;
            var totalEmployerContributions = 0; //1190.35d;
            var employerMedicalAidContribution = 0; // 1726.5d;
            var travelAllowance = 0;
            var travelAllowanceInclusionPercentage = 0.8d;
            var totalOtherAllowances = 0;
            var paymentRefrequency = 12;
            var sumOfEmployeeContribution = 1000d;
            var sumOfEmployerContribution = 430.95d;
            var limitPercentageForRetirementFundsContributions = 27.5d;
            var maximumRetirementFundsContributions = 350000d;
            var mainMemberMedicalAidCredit = 310d;
            var firstDependentMedicalAidCredit = 310d;
            var otherDependentMedicalAidCredit = 209d;
            var numberOfDependents = 0;
            var slidingScale = 147891d;
            var taxPercentage = 39d;
            var minimumNonTaxableAmount = 555600d;
            var taxRebate = 14220d;
            var uifLimit = 148.72d;

            var grossRenumeration = CalculateGrossRemuneration(basicSalary, sumOfEmployerContribution, employerMedicalAidContribution, travelAllowance, travelAllowanceInclusionPercentage, totalOtherAllowances);
            var annualGrossRenumeration = CalculateAnnualAmount(paymentRefrequency, grossRenumeration);
            var retirementFundsContribution = CalculateRetirementFundsContribution(sumOfEmployeeContribution, sumOfEmployerContribution);
            var annualRetirementFundsContribution = CalculateAnnualAmount(paymentRefrequency, retirementFundsContribution);
            var limitOfRetirementFundsContribution = CalculateLimitOfRetirementFundsContribution(limitPercentageForRetirementFundsContributions, annualGrossRenumeration);
            var absoluteLimitOfRetirementFundsContribution = CalculateAbsoluteLimitOfRetirementFundsContribution(limitOfRetirementFundsContribution, maximumRetirementFundsContributions);
            var retirementFundsContributionDeduction = CalculateRetirementFundsContributionDeduction(annualRetirementFundsContribution, limitOfRetirementFundsContribution, absoluteLimitOfRetirementFundsContribution);
            var taxableIncome = CalculateTaxableIncome(annualGrossRenumeration, retirementFundsContributionDeduction);

            var firstDepandantMedicalAidTaxCredit = CalculateFirstDepandantMedicalAidTaxCredit(firstDependentMedicalAidCredit, numberOfDependents);
            var additionalDepandantMedicalAidTaxCredit = CalculateAdditionalDepandantMedicalAidTaxCredit(otherDependentMedicalAidCredit, numberOfDependents);
            var medicalAidTaxCredit = CalculateMedicalAidTaxCredit(additionalDepandantMedicalAidTaxCredit, mainMemberMedicalAidCredit, firstDepandantMedicalAidTaxCredit, numberOfDependents);


            var annualTaxAmount = CalculateAnnualTaxAmount(taxableIncome, slidingScale, taxPercentage, minimumNonTaxableAmount, taxRebate);
            var monthlyTaxAmount = CalculatePaymentFrequencyTaxAmount(paymentRefrequency, annualTaxAmount, medicalAidTaxCredit);
            var uifAmount = CalculateUIFAmount(grossRenumeration, uifLimit);
            var takeHomeAmount = CalculateTakeHomeAmount(grossRenumeration, uifAmount, monthlyTaxAmount, retirementFundsContributionDeduction, paymentRefrequency);

            System.Console.WriteLine($"Gross Renumeration: {grossRenumeration}");
            System.Console.WriteLine($"Annual Gross Renumeration: {annualGrossRenumeration}");
            System.Console.WriteLine($"Medical Aid Tax Credit: {medicalAidTaxCredit}");
            System.Console.WriteLine($"Annual Taxable Income: {taxableIncome}");
            System.Console.WriteLine($"Annual Tax Amount: {annualTaxAmount}");
            System.Console.WriteLine($"Monthly Tax Amount: {monthlyTaxAmount}");
            System.Console.WriteLine($"Take Home Amount: {takeHomeAmount}");
            Console.ReadLine();
        }

        static double CalculateGrossRemuneration(double basicSalary, double totalEmployerContributions, double employerMedicalAidContribution, double travelAllowance,
            double travelAllowanceInclusionPercentage, double totalOtherAllowances)
        {

            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("basic_salary", basicSalary));
            arguments.Add(new Argument("total_employer_contributions", totalEmployerContributions));
            arguments.Add(new Argument("employer_medical_aid_contribution", employerMedicalAidContribution));
            arguments.Add(new Argument("travel_allowance", travelAllowance));
            arguments.Add(new Argument("travel_allowance_inclusion_percentage", travelAllowanceInclusionPercentage));
            arguments.Add(new Argument("total_other_allowances", totalOtherAllowances));

            string grossRenumerationFormula = "basic_salary + total_employer_contributions + employer_medical_aid_contribution + (travel_allowance * travel_allowance_inclusion_percentage) + total_other_allowances";

            return ExecuteFormula(grossRenumerationFormula, arguments.ToArray());
        }

        static double CalculateAnnualAmount(double paymentFrequency, double amount)
        {

            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("payment_frequency", paymentFrequency));
            arguments.Add(new Argument("amount", amount));

            string annualAmountFormula = "payment_frequency * amount";

            return ExecuteFormula(annualAmountFormula, arguments.ToArray());
        }

        static double CalculateRetirementFundsContribution(double sumOfEmployerContribution, double sumOfEmployeeContribution)
        {

            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("employer_contribution", sumOfEmployerContribution));
            arguments.Add(new Argument("employee_contribution", sumOfEmployeeContribution));

            string retirementFundsContributionFormula = "employer_contribution + employee_contribution";

            return ExecuteFormula(retirementFundsContributionFormula, arguments.ToArray());
        }

        static double CalculateLimitOfRetirementFundsContribution(double limitPercentageForRetirementFundsContributions, double annualRetirementFundsContribution)
        {

            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("limit_percentage", limitPercentageForRetirementFundsContributions));
            arguments.Add(new Argument("annual_retirement_funds_contribution", annualRetirementFundsContribution));

            string retirementFundsContributionFormula = "(limit_percentage/100) * annual_retirement_funds_contribution";

            return ExecuteFormula(retirementFundsContributionFormula, arguments.ToArray());
        }

        static double CalculateAbsoluteLimitOfRetirementFundsContribution(double limitOfRetirementFundsContribution, double maximumRetirementFundsContributions)
        {

            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("limit_ra_amount", limitOfRetirementFundsContribution));
            arguments.Add(new Argument("max_ra_amount", maximumRetirementFundsContributions));

            string retirementFundsContributionFormula = "min(max_ra_amount, limit_ra_amount)";

            return ExecuteFormula(retirementFundsContributionFormula, arguments.ToArray());
        }
        
        static double CalculateRetirementFundsContributionDeduction(double annualRetirementFundsContribution, double limitOfRetirementFundsContribution, double absoluteLimitOfRetirementFundsContribution)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("limit_ra_amount", limitOfRetirementFundsContribution));
            arguments.Add(new Argument("annual_ra_amount", annualRetirementFundsContribution));
            arguments.Add(new Argument("absolute_limit_ra_limit", absoluteLimitOfRetirementFundsContribution));

            string retirementFundsContributionDeductionFormula = "min(annual_ra_amount, limit_ra_amount, absolute_limit_ra_limit)";

            return ExecuteFormula(retirementFundsContributionDeductionFormula, arguments.ToArray());
        }

        static double CalculateTaxableIncome(double annualGrossRenumeration, double retirementFundsContributionDeduction)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("annual_gross_renumeration", annualGrossRenumeration));
            arguments.Add(new Argument("retirement_funds_contribution_deduction", retirementFundsContributionDeduction));;

            string taxableIncomeFormula = "annual_gross_renumeration - retirement_funds_contribution_deduction";

            return ExecuteFormula(taxableIncomeFormula, arguments.ToArray());
        }

        static double CalculateFirstDepandantMedicalAidTaxCredit(double firstDependentMedicalAidCredit, int numberOfDependents)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("first_dependent_medical_aid_credit", firstDependentMedicalAidCredit));
            arguments.Add(new Argument("number_of_dependents", numberOfDependents)); ;

            string firstDepandantTaxCreditFormula = "first_dependent_medical_aid_credit * (if(number_of_dependents > 1, 1, 0))";

            return ExecuteFormula(firstDepandantTaxCreditFormula, arguments.ToArray());
        }

        static double CalculateAdditionalDepandantMedicalAidTaxCredit(double otherDependentMedicalAidCredit, int numberOfDependents)
        {
            List<Argument> arguments = new List<Argument>();

            if (numberOfDependents - 2 < 0)
            {
                numberOfDependents = 0;
            }

            arguments.Add(new Argument("other_dependent_medical_aid_credit", otherDependentMedicalAidCredit));
            arguments.Add(new Argument("number_of_dependents", numberOfDependents)); ;

            string additionalDepandantTaxCreditFormula = "max(other_dependent_medical_aid_credit * (number_of_dependents-2), 0)";

            return ExecuteFormula(additionalDepandantTaxCreditFormula, arguments.ToArray());
        }

        static double CalculateMedicalAidTaxCredit(double additionalDepandantMedicalAidTaxCredit, double mainMemberMedicalAidCredit, double firstDependentMedicalAidCredit, int numberOfDependents)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("additional_depandant_medical_aid_tax_credit", additionalDepandantMedicalAidTaxCredit));
            arguments.Add(new Argument("main_member_medical_aid_credit", mainMemberMedicalAidCredit)); 
            arguments.Add(new Argument("first_dependent_medical_aid_credit", firstDependentMedicalAidCredit));
            arguments.Add(new Argument("number_of_dependents", numberOfDependents)); ;

            string medicalAidTaxCreditFormula = "additional_depandant_medical_aid_tax_credit + (number_of_dependents*main_member_medical_aid_credit) + first_dependent_medical_aid_credit";

            return ExecuteFormula(medicalAidTaxCreditFormula, arguments.ToArray());
        }

        static double CalculateAnnualTaxAmount(double taxableIncome, double slidingScale, double taxPercentage, double minimumNonTaxableAmount, double taxRebate)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("taxable_income", taxableIncome));
            arguments.Add(new Argument("sliding_scale", slidingScale));
            arguments.Add(new Argument("tax_percentage", taxPercentage));
            arguments.Add(new Argument("min_taxable_amount", minimumNonTaxableAmount));
            arguments.Add(new Argument("tax_rebate", taxRebate));

            string annualTaxAmountFormula = "sliding_scale + (taxable_income - min_taxable_amount)*(tax_percentage/100) - tax_rebate";

            return ExecuteFormula(annualTaxAmountFormula, arguments.ToArray());
        }

        static double CalculatePaymentFrequencyTaxAmount(double paymentFrequency, double amount, double medicalAidTaxCredit)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("payment_frequency", paymentFrequency));
            arguments.Add(new Argument("annual_tax_amount", amount));
            arguments.Add(new Argument("medical_aid_tax_credit", medicalAidTaxCredit));

            string paymentFrequencyTaxAmountFormula = "annual_tax_amount/payment_frequency - medical_aid_tax_credit";

            return ExecuteFormula(paymentFrequencyTaxAmountFormula, arguments.ToArray());
        }

        static double CalculateUIFAmount(double grossIncome, double maximumUifAmount)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("gross_amount", grossIncome));
            arguments.Add(new Argument("max_uif_amount", maximumUifAmount));

            string uifFormula = "min(gross_amount*0.01, max_uif_amount)";

            return ExecuteFormula(uifFormula, arguments.ToArray());
        }

        static double CalculateTakeHomeAmount(double grossIncome, double uifAmount, double taxableAmount, double retirementFundsContributionDeduction, double paymentRefrequency)
        {
            List<Argument> arguments = new List<Argument>();

            arguments.Add(new Argument("gross_income", grossIncome));
            arguments.Add(new Argument("uif_amount", uifAmount));
            arguments.Add(new Argument("taxable_amount", taxableAmount));
            arguments.Add(new Argument("retirement_funds_contribution_deduction", retirementFundsContributionDeduction));
            arguments.Add(new Argument("payment_frequency", paymentRefrequency));

            string takeHomeFormula = "gross_income - uif_amount - taxable_amount -(retirement_funds_contribution_deduction / payment_frequency)";

            return ExecuteFormula(takeHomeFormula, arguments.ToArray());
        }

        static double ExecuteFormula(string formular, params Argument[] arguments)
        {
            Expression expression = new Expression(formular);

            expression.addArguments(arguments);

            if (!expression.checkSyntax())
            {
                throw new MathParserFormulaException($"{ formular } failed to execute please verify that all arguement are passed in to the formular");
            }

            return expression.calculate();
        }
    }
}
