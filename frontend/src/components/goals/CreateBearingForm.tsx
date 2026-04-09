import { Alert, Button, Group, Stack, Textarea, TextInput } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { useState } from "react";
import { IconExclamationCircle } from "@tabler/icons-react";
import { postApiGoalCreateBearing } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateBearingBody } from "@/api/endpoints/goal/goal.zod.js";

export default function CreateBearingForm() {
    const form = useForm({
        mode: 'uncontrolled',
        validate: schemaResolver(PostApiGoalCreateBearingBody, { sync: true })
    })
    const [alert, setAlert] = useState("");

    const handleSubmit = async (values: typeof form.values) => {
        const response = await postApiGoalCreateBearing(values);

        if (response.status == 200) {
            close();
        } else {
            setAlert(response.data);
        }
    };

    return (
        <>
            <Alert variant="light" color="red" title="Error" icon={<IconExclamationCircle />} hidden={alert == ""}>{alert}</Alert>
            <form onSubmit={form.onSubmit(handleSubmit, (errors) => console.log(errors))}>
                <Stack>
                    <TextInput
                        label="Name"
                        description="What is your goal?"
                        placeholder="Be healthy"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />
                    <Textarea
                        label="Description"
                        description="What does your goal consist of?"
                        placeholder="Be healthy"
                        required
                        key={form.key('description')}
                        {...form.getInputProps('description')}
                    />
                    <Textarea
                        label="Justification"
                        description="How does this goal help you achieve your ideal self? Use research."
                        placeholder="Be healthy"
                        key={form.key('justification')}
                        {...form.getInputProps('justification')}
                    />
                    <Textarea
                        label="Difficulty"
                        description="How difficult will this goal be? Are you ready to take it on?"
                        placeholder="Be healthy"
                        key={form.key('difficulty')}
                        {...form.getInputProps('difficulty')}
                    />

                    <Group justify="flex-end" mt="md">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}