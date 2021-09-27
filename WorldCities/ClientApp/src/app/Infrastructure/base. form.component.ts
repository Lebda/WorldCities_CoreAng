import { AbstractControl, FormGroup } from "@angular/forms";

export abstract class BaseFormComponent {
  // the form model
  public form: FormGroup;

  protected constructor(formCreator: () => FormGroup) {
    this.form = formCreator();
  }

  // retrieve a FormControl
  public getControl(name: string): AbstractControl | null {
    if (!this.form) {
      return null;
    }
    return this.form.get(name);
  }

  // returns TRUE if the FormControl is valid
  public isValid(name: string): boolean {
    const e = this.getControl(name);
    if (!e) {
      return false;
    }
    return e && e.valid;
  }
  // returns TRUE if the FormControl has been changed
  public isChanged(name: string): boolean {
    const e = this.getControl(name);
    if (!e) {
      return false;
    }
    return e && (e.dirty || e.touched);
  }
  // returns TRUE if the FormControl is raising an error,
  // i.e. an invalid state after user changes
  public hasError(name: string): boolean {
    const e = this.getControl(name);
    if (!e) {
      return false;
    }
    return e && (e.dirty || e.touched) && e.invalid;
  }
}
