export * from './UIService';
export * from './auth.guard';
export * from './auth.service';
export * from './can-deactivate.guard';
export * from './error.interceptor';
export * from './jwt.interceptor';
export * from './profile.service';
export * from './routes.constants';

export class Format {
  public static Time(time: string): string {
    if (time === "") {
      return "";
    }
    let hh: number = 0;
    let mm: number = 0;
    let AM: boolean = false;
    let arr = time.split(":");
    hh = Number(arr[0]);
    if (hh === 0) {
      AM = true
      hh = 12;
    } else if (hh < 12) {
      AM = true;
    } else if (hh > 12) {
      hh = hh - 12;
    }

    return `${hh < 10 ? "0" + hh : hh}:${arr[1]} ${AM ? "AM" : "PM"}`;
  }
}
