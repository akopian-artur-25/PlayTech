import {FormControl} from '@angular/forms';

export class FormValidators {

  public static autocompleteRequired(control: FormControl) {
    return (!control.value || !control.value.key) ?
      {'required': true} :
      null;
  }

  public static requiredTrim(control: FormControl) {
    return (typeof(control.value) !== 'string' || control.value.trim() === '') ?
      {'required': true} :
      null;
  }

  public static patternTrim(pattern: string) {
    return (control: FormControl) => {
      const regExp = new RegExp(pattern);
      return (typeof(control.value) !== 'string' || control.value.trim() === '' || !regExp.test(control.value)) ?
        {'pattern': true} :
        null;
    };
  }

  public static minLength(length: number) {
    return (control: FormControl) => {
      if (!control.value || typeof control.value === 'string' && control.value.trim().length <= length) {
        return {
          'minlength': true
        };
      }
      return null;
    };
  }

  public static maxLength(length: number) {
    return (control: FormControl) => {
      if (control.value && typeof control.value === 'string' && control.value.trim().length >= length) {
        return {
          'maxlength': true
        };
      }

      return null;
    };
  }


  public static email(control: FormControl) {
    const regExp = new RegExp('^((([a-zA-Z]|\\d|[!#\\$%&\'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\\.([a-zA-Z]|\\d|[!#\\$%&\'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-zA-Z]|\\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|\\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|\\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\\.)+(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-zA-Z]+|\\d|-|\\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$');

    return (typeof(control.value) !== 'string' || control.value.trim() === '' || !regExp.test(control.value)) ?
      {'email': true} :
      null;
  }
}
