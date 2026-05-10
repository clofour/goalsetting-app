import { Button, Checkbox, Group, Input, NumberInput, SegmentedControl, Select, Stack, TextInput, useCombobox } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { DatePickerInput, DateTimePicker, TimePicker } from "@mantine/dates";
import { getErrorMessage } from "@/data/error";
import { useState } from "react";
import { postApiEventCreateOnetime, postApiEventCreateRecurring } from "@/api/endpoints/event/event";
import { durationToMinutes } from "@/helpers";
import { RecurrenceTypes, Weekday } from "@/api/models";
import { PostApiEventCreateOnetimeBody, PostApiEventCreateRecurringBody } from "@/api/endpoints/event/event.zod";

interface EventFormProps {
    close: () => void;
    setAlert: (alert: string) => void;
}

export default function EventForm({ close, setAlert }: EventFormProps) {
    const form = useForm({
        mode: 'controlled',
        initialValues: {
            'name': '',
            'start': '',
            'duration': '',
            'type': 'onetime',
            'recurrenceAmount': 1,
            'recurrenceType': RecurrenceTypes.WEEKLY as RecurrenceTypes,
            'weekDays': [],
            'monthDay': '',
            'yearMonth': '',

        },
        // validate: schemaResolver(formSchema, { sync: true })
    })
    const handleSubmit = async (values: typeof form.values) => {
        const requestData = {
            ...values,
            start: new Date(values.start).toISOString(),
            duration: durationToMinutes(values.duration)
        }

        let response;

        switch (values.type) {
            case "recurring":
                response = await postApiEventCreateRecurring(requestData);
                break;

            case "onetime":
                response = await postApiEventCreateOnetime(requestData);
                break;

            default:
                throw RangeError("Event type must be either 'recurring' or 'onetime'.")
        }

        if (response.status === 200) {
            close();
        } else {
            setAlert(response.data ?? getErrorMessage(response.status));
        }
    };

    const eventTypes = [
        { label: "One-time", value: "onetime" },
        { label: "Recurring", value: "recurring" }
    ]
    const unitCombobox = useCombobox({
        onDropdownClose: () => unitCombobox.resetSelectedOption()
    })
    const unitOptions = [
        { label: "day", value: RecurrenceTypes.DAILY },
        { label: "week", value: RecurrenceTypes.WEEKLY },
        { label: "month", value: RecurrenceTypes.MONTHLY },
        { label: "year", value: RecurrenceTypes.YEARLY }
    ];
    const weekdays = [
        { label: "Monday", value: Weekday.MO },
        { label: "Tuesday", value: Weekday.TU },
        { label: "Wednesday", value: Weekday.WE },
        { label: "Thursday", value: Weekday.TH },
        { label: "Friday", value: Weekday.FR },
        { label: "Saturday", value: Weekday.SA },
        { label: "Sunday", value: Weekday.SU }
    ];

    return (
        <>
            <form onSubmit={form.onSubmit(handleSubmit, (errors) => console.log(errors))}>
                <Stack>
                    <TextInput
                        label="Name"
                        description="What should this event be called?"
                        placeholder="Go to the gym"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />

                    <DateTimePicker
                        label="Start date"
                        description="When should this event start?"
                        placeholder="Tomorrow"
                        required
                        key={form.key('start')}
                        {...form.getInputProps('start')}
                    />

                    <TimePicker
                        label="Duration"
                        description="How long should this event last?"
                        type="duration"
                        required
                        key={form.key('duration')}
                        {...form.getInputProps('duration')}
                    />

                    <Input.Wrapper
                        label="Type"
                        description="Should this event be recurring?"
                    >
                        <SegmentedControl

                            data={eventTypes}
                            fullWidth
                            key={form.key('type')}
                            {...form.getInputProps('type')}
                        />
                    </Input.Wrapper>

                    {form.values.type == "recurring" && (
                        <>
                            <Group grow justify="flex-between">
                                <NumberInput
                                    label="Amount"
                                    description="How many recurrence units should there be?"
                                    placeholder="1"
                                    required
                                    key={form.key('recurrenceAmount')}
                                    {...form.getInputProps('recurrenceAmount')}
                                />
                                <Select
                                    label="Unit"
                                    description="What should the recurrence unit be?"
                                    required
                                    data={unitOptions}
                                    key={form.key('recurrenceType')}
                                    {...form.getInputProps('recurrenceType')}
                                />
                            </Group>

                            {form.values.recurrenceType == RecurrenceTypes.WEEKLY && (
                                <Checkbox.Group
                                    label="Day of the week"
                                    description="Which day of the week should this event take place?"
                                    required
                                    key={form.key('weekDays')}
                                    {...form.getInputProps('weekDays')}
                                >
                                    <Group mt="xs">
                                        {weekdays.map((weekday) => (
                                            <Checkbox key={weekday.value} label={weekday.label} value={weekday.value} />
                                        ))}
                                    </Group>
                                </Checkbox.Group>
                            )}

                            {form.values.recurrenceType == RecurrenceTypes.MONTHLY && (
                                <NumberInput
                                    label="Day of the month"
                                    description="Which day of the month should this event take place?"
                                    min={1}
                                    max={31}
                                    required
                                    key={form.key('monthDay')}
                                    {...form.getInputProps('monthDay')}
                                />
                            )}

                            {form.values.recurrenceType == RecurrenceTypes.YEARLY && (
                                <Group grow justify="flex-between">
                                    <NumberInput
                                        label="Day of the month"
                                        description="Which day of the month should this event take place?"
                                        required
                                        key={form.key('monthDay')}
                                        {...form.getInputProps('monthDay')}
                                    />
                                    <NumberInput
                                        label="Month of the year"
                                        description="Which month of the eyar should this event take place?"
                                        required
                                        key={form.key('yearMonth')}
                                        {...form.getInputProps('yearMonth')}
                                    />
                                </Group>
                            )}
                        </>
                    )}

                    <Group justify="flex-end">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}