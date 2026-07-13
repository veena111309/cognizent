export interface OrgUnit {
  id: number;
  name: string;
}

export interface TechnicalSkill {
  id: number;
  name: string;
}

export interface EmployeeRecord {
  id: number;
  name: string;
  salary: number;
  permanent: boolean;
  department: OrgUnit;
  skills: TechnicalSkill[];
  dateOfBirth: Date;
}
