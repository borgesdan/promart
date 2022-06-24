namespace Promart.API
{
    /// <summary>
    /// [0] Masculino
    /// [1] Feminino
    /// [3] Não Informado
    /// </summary>
    public enum GenderType : byte
    {
        Male = 0,
        Female = 1,
        Undefined = 2
    }

    /// <summary>
    /// Define com quem o aluno reside junto, a relação mais superior.
    /// [0] Pais
    /// [1] Avós
    /// [2] Mãe
    /// [3] Pai
    /// [4] Mãe-Padrasto
    /// [5] Pai-Madrasta
    /// [6] Tios
    /// [7] Outro
    /// [8] Não Informado
    /// </summary>
    public enum FamilyRelationshipType : byte
    {
        Parents = 0,
        Grandparents = 1,
        OnlyMother = 2,
        OnlyFather = 3,
        MotherStepfather = 4,
        FatherStepmother = 5,
        Uncles = 6,
        Other = 7,
        Undefined = 8,
    }

    /// <summary>
    /// Obtém ou define o turno escolar.
    /// [0] Matutino
    /// [1] Vespertino
    /// [3] Não Informado
    /// </summary>
    public enum SchoolShiftType : byte
    {
        Morning = 0,
        Afternoon = 1,
        Undefined = 2
    }

    /// <summary>
    /// Obtém ou define o tipo de moradia.
    /// [0] Própria
    /// [1] Alugada
    /// [2] Cedida pela família
    /// [3] Cedida pelo empregador
    /// [4] Cedida de outra forma
    /// [5] Outro tipo
    /// [6] Não Informado
    /// </summary>
    public enum DwellingType : byte
    {
        /// <summary>Própria.</summary>
        Own = 0,
        /// <summary>Alugada.</summary>
        Rented = 1,
        /// <summary>Cedida pela família.</summary>
        FamilyProvided = 2,
        /// <summary>Cedida pelo empregador.</summary>
        EmployerProvided = 3,
        /// <summary>Cedida de outra forma.</summary>
        OtherProvided = 4,
        /// <summary>Outra forma.</summary>
        Other = 5,
        Undefined = 6,
    }

    /// <summary>
    /// Obtém ou define a faixa salárial da renda mensal.
    /// [0] Menor que 1/2 Salário Mínimo
    /// [1] 1/2 SM
    /// [2] 1 SM
    /// [3] 1 e 1/2 SM
    /// [4] 2 SM
    /// [5] Maior que 2 SM
    /// [6] Não Informado
    /// </summary>      
    public enum MonthlyIncomeType : byte
    {
        LessHalfMinimumSalary = 0,
        HalfMinimumSalary = 1,
        MinimumSalary = 2,
        MinimumSalaryAndHalf = 3,
        TwoMinimumSalary = 4,
        GreaterTwoMinimumSalary = 5,
        Undefined = 6
    }

    /// <summary>
    /// [0] 1º Ano Ensino Fundamental
    /// [1] 2º Ano Ensino Fundamental
    /// [2] 3º Ano Ensino Fundamental
    /// [3] 4º Ano Ensino Fundamental
    /// [4] 5º Ano Ensino Fundamental
    /// [5] 6º Ano Ensino Fundamental
    /// [6] 7º Ano Ensino Fundamental
    /// [7] 8º Ano Ensino Fundamental
    /// [8] 9º Ano Ensino Fundamental
    /// [9] 1º Ano Ensino Fundamental
    /// [10] 1º Ano Ensino Médio
    /// [11] 2º Ano Ensino Médio
    /// [12] 3º Ano Ensino Médio
    /// [13] 4º Ano Ensino Médio
    /// [14] Não Informado
    /// </summary>      
    public enum SchoolYearType : byte
    {
        Elementary1 = 0,
        Elementary2 = 1,
        Elementary3 = 2,
        Elementary4 = 3,
        Elementary5 = 4,
        Elementary6 = 5,
        Elementary7 = 6,
        Elementary8 = 7,
        Elementary9 = 8,
        High1 = 9,
        High2 = 10,
        High3 = 11,
        High4 = 12,
        Undefined = 13
    }

    /// <summary>
    /// Obtém ou define a situação do aluno no projeto.
    /// [0] Inscrito
    /// [1] Aprovado
    /// [2] Em Espera
    /// [3] Matriculado
    /// [4] Não Aprovado
    /// [5] Desistente
    /// [6] Ex-aluno
    /// [7] Não Especificado
    /// </summary>
    public enum StudentProjectStatusType : byte
    {
        /// <summary>Inscrito</summary>
        Registered = 0,
        /// <summary>Aprovado</summary>
        Approved = 1,
        /// <summary>Em espera</summary>
        Waiting = 2,
        /// <summary>Matriculado</summary>
        Matriculate = 3,
        /// <summary>Não aprovado</summary>
        NotApproved = 4,
        /// <summary>Desistente</summary>
        Quitter = 5,
        /// <summary>Ex aluno</summary>
        ExStudent = 6,        
        Undefined = 7
    }
    
    public enum PersonType : byte
    {
        Student = 0,
        Voluntary = 1
    }
}
