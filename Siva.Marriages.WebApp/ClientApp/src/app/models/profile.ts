export interface CandidateProfile {
  id: string,
  data: ProfileData,
  picturesId: string[]
}

export interface ProfileData {
  name: string,
  surname: string,
  motherMaidenname: string,
  nativePlace: string,
  gender: Gender,
  contactDetails: ContactDetails,
  birthDetails: BirthDetails,
  profession: Profession,
  education: Education,
  height: string,
  otherDetails: string,
  father: string,
  mother: string,
  siblings: SiblingsProfile[],
  referrer: string
}

export interface ContactDetails {
  number1: Number,
  number2: Number,
  email: String
}

export interface BirthDetails {
  dateOfBirth: string,
  timeOfBirth: string,
  placeOfBirth: string,
  rasi: Zodiac,
  nakshatra: Nakshatra,
}

export interface SiblingsProfile {
  details: string
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
