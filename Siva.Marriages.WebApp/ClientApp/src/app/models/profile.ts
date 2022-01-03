export interface CandidateProfile {
  id: string,
  data: ProfileData,
  picturesId: string[]
}

export interface ProfileData extends BaseProfile {
  surname: string,
  motherMaidenname: string,
  birthDetails: BirthDetails,
  nativePlace: string,
  father: BaseProfile,
  mother: BaseProfile,
  siblings: SiblingsProfile[],
  height: string
}

export interface BaseProfile {
  name: string,
  gender: Gender,
  contactDetails: ContactDetails,
  profession: Profession,
  education: Education,
  otherDetails: string
}

export interface ContactDetails {
  number1: Number,
  number2: Number,
  email: String
}

export interface BirthDetails {
  dateOfBirth: Date,
  timeOfBirth: Date,
  placeOfBirth: string,
  rasi: Zodiac,
  nakshatra: Nakshatra,
}

export interface SiblingsProfile extends BaseProfile {
  maritalStatus: Marital,
  elder: Elder
}

export enum Gender {
  Male,
  Female
}

export enum Elder {
  Elder,
  Younger,
  Twin
}

export enum Marital {
  Married,
  Single,
  Widowed,
  Separated,
  Divorced
}

export interface Profession {
  designation: string,
  companyName: string,
  salary: string,
  place: string
}

export interface Education {
  name: string,
  institute: string,
  location: string,
}

export enum Zodiac {
  Mesha,
  Vrishabha,
  Mithuna,
  Karkataka,
  Simha,
  Kanya,
  Tula,
  Vrischika,
  Dhanu,
  Makara,
  Kumbha,
  Mina
}

export enum Nakshatra {
  Aswini,
  Bharani,
  Kritika,
  Rohini,
  Mrigasiraa,
  Arudra,
  Punarvasu,
  Pushya,
  Ashlesha,
  Maghaa,
  Pubba,
  Uttara,
  Hasta,
  Chittaa,
  Swaati,
  Vishaakha,
  Anuraadha,
  Jyesthaa,
  Moolaa,
  Poorvashaadha,
  Uttarashaadha,
  Shravana,
  Dhanishta,
  Shatabhisha,
  Poorvabhadra,
  Uttaraabhadra,
  Revati
}
